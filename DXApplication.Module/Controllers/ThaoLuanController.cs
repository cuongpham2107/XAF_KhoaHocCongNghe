using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DXApplication.Blazor.BusinessObjects;
using DXApplication.Module.BusinessObjects.Main;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{

    public partial class ThaoLuanController : ViewController
    {

        public ThaoLuanController()
        {
            InitializeComponent();
            Btn_ThaoLuan();
        }
        public void Btn_ThaoLuan()
        {
            var action = new PopupWindowShowAction(this, $"{Btn_ThaoLuan}", "Edit")
            {
                Caption = "Thảo luận",
                ImageName = "Glyph_Message",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(ThaoLuan),
                SelectionDependencyType = SelectionDependencyType.Independent,
            };
            action.CustomizePopupWindowParams += Btn_CustomizePopupWindowParams;
            action.Execute += Btn_NoiDungThaoLuan;

        }
        public void Btn_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace os = (NonPersistentObjectSpace)e.Application.CreateObjectSpace(typeof(ThaoLuanParameter));
            os.PopulateAdditionalObjectSpaces(Application);
            e.DialogController.SaveOnAccept = false;
            e.View = e.Application.CreateDetailView(os, os.CreateObject<ThaoLuanParameter>());
        }
        public void Btn_NoiDungThaoLuan(object s, PopupWindowShowActionExecuteEventArgs e)
        {
            if (((DetailView)ObjectSpace.Owner).CurrentObject is Noidung_DeTai nd)
            {
                var parameter = ((ThaoLuanParameter)e.PopupWindowViewCurrentObject);
                var _noidung = ObjectSpace.GetObject(nd);
                var os = Application.CreateObjectSpace(typeof(DeTaiDuAn_KHCN));
                var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
                ThaoLuan tl = ObjectSpace.CreateObject<ThaoLuan>();

                var hoTenUser = account.ChuNhiem_CanBoQuanLy.HoTen;
                if (hoTenUser == null)
                {
                    tl.Ten = "User";
                }
                else
                {
                    tl.Ten = hoTenUser.ToString();
                }
                tl.NoiDung = parameter.ThaoLuan;
                tl.HinhAnh = parameter.Image;

                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                using (var stream = new MemoryStream())
                {
                    parameter.File.SaveToStream(stream);
                    stream.Position = 0;
                    fileCopy.LoadFromStream(parameter.File.FileName, stream);
                }

                tl.File = fileCopy;

                tl.Noidung_DeTai = _noidung;
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Thêm thảo luận thành công!", InformationType.Success);
            }
        }

    }
    [DomainComponent]
    [XafDisplayName("Thảo luận")]
    public class ThaoLuanParameter : NonPersistentObjectImpl, IDomainComponent
    {
        [XafDisplayName("Nội dung cần thảo luận")]
        [RuleRequiredField($"Bắt buộc phải có {nameof(ThaoLuanParameter.ThaoLuan)}", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string ThaoLuan { get; set; }

        [XafDisplayName("Tệp đính kèm ")]
        public virtual FileData File { get; set; }

        [XafDisplayName("Hình ảnh")]
        [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
        public byte[] Image { get; set; }
    }
}
