using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
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
using DomainComponents.Common;
using DXApplication.Blazor.BusinessObjects;
using DXApplication.Module.BusinessObjects.Main;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{

    public partial class PheDuyetDeTaiController : ViewController
    {

        public PheDuyetDeTaiController()
        {
            InitializeComponent();
            Btn_PheDuyetDeTai();
            Btn_HuyPheDuyet();
            Btn_NghiemVuKetThuc();
            Btn_DinhKemTaiLieu();
        }
        public void Btn_DinhKemTaiLieu()
        {
            var action = new PopupWindowShowAction(this, $"{nameof(Btn_DinhKemTaiLieu)}", "Edit")
            {
                Caption = "Thêm tài liệu nghiệm thu",
                ImageName = "ApplyChanges",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(FileDuLieu),
                TargetObjectsCriteria = "[DeTaiDuAn_KHCN.TrangThaiDeTai] <> ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,nhiemvudaketthuc#",
                SelectionDependencyType = SelectionDependencyType.Independent,
            };
            action.CustomizePopupWindowParams += Btn_CustomizePopupWindowParams;
            action.Execute += Btn_TepDinhKem;

        }
        public void Btn_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace os = (NonPersistentObjectSpace)e.Application.CreateObjectSpace(typeof(TaiLieuParameter));
            os.PopulateAdditionalObjectSpaces(Application);
            e.DialogController.SaveOnAccept = false;
            e.View = e.Application.CreateDetailView(os, os.CreateObject<TaiLieuParameter>());
        }
        public void Btn_TepDinhKem(object s, PopupWindowShowActionExecuteEventArgs e)
        {
            if (((DetailView)ObjectSpace.Owner).CurrentObject is DeTaiDuAn_KHCN dt)
            {
                var parameter = ((TaiLieuParameter)e.PopupWindowViewCurrentObject);
                var _detai = ObjectSpace.GetObject(dt);

                FileDuLieu f = ObjectSpace.CreateObject<FileDuLieu>();
                f.TenFile = parameter.TenFile;

                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                using (var stream = new MemoryStream())
                {
                    parameter.File.SaveToStream(stream);
                    stream.Position = 0;
                    fileCopy.LoadFromStream(parameter.File.FileName, stream);
                }

                f.File = fileCopy;
                f.DeTaiDuAn_KHCN = _detai;
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Thêm tài liệu nghiệm thu thành công!", InformationType.Success);
            }
        }
        public void Btn_PheDuyetDeTai()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_PheDuyetDeTai)}", "Edit")
            {
                Caption = "Phê duyệt",
                ImageName = "TrackingChanges_Accept",
                TargetViewNesting = Nesting.Root,
                TargetViewType = ViewType.DetailView,
                TargetObjectType = typeof(DeTaiDuAn_KHCN),
                TargetObjectsCriteria = "[TrangThaiDeTai] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,chuaduocduyet#",
                ConfirmationMessage = "Xác nhận phê duyệt đề tài này!",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            };
            action.Execute += (s, e) =>
            {
                try
                {
                    DeTaiDuAn_KHCN detai = (DeTaiDuAn_KHCN)View.CurrentObject;
                    detai.TrangThaiDeTai = Blazor.Common.Enums.TrangThaiDeTai.duyet;
                    this.ObjectSpace.CommitChanges();
                    Application.ShowViewStrategy.ShowMessage("Phê duyệt đề tài này thành công!", InformationType.Success);
                }
                catch (Exception)
                {
                    Application.ShowViewStrategy.ShowMessage("Phê duyệt đề tài này thất bại!", InformationType.Error);
                    throw;
                }

            };
        }
        public void Btn_HuyPheDuyet()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_HuyPheDuyet)}", "Edit")
            {
                Caption = "Huỷ Phê duyệt",
                ImageName = "SnapDeleteList",
                TargetViewNesting = Nesting.Root,
                TargetViewType = ViewType.DetailView,
                TargetObjectType = typeof(DeTaiDuAn_KHCN),
                TargetObjectsCriteria = "[TrangThaiDeTai] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,duyet#",
                ConfirmationMessage = "Xác nhận huỷ phê duyệt đề tài này!",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            };
            action.Execute += (s, e) =>
            {
                DeTaiDuAn_KHCN detai = (DeTaiDuAn_KHCN)View.CurrentObject;
                detai.TrangThaiDeTai = Blazor.Common.Enums.TrangThaiDeTai.chuaduocduyet;
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Huỷ duyệt đề tài này thành công!", InformationType.Success);
            };
        }
        public void Btn_NghiemVuKetThuc()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_NghiemVuKetThuc)}", "Edit")
            {
                Caption = "Nghiệm thu",
                ImageName = "ProtectDocument",
                TargetViewNesting = Nesting.Root,
                TargetViewType = ViewType.DetailView,
                TargetObjectType = typeof(DeTaiDuAn_KHCN),
                TargetObjectsCriteria = "[TrangThaiBool] = True And [TrangThaiDeTai] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,duyet#",
                ConfirmationMessage = "Xác nhận đã kết thúc nghiệm vụ của đề tài này!",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            };
            action.Execute += (s, e) =>
            {
                DeTaiDuAn_KHCN detai = (DeTaiDuAn_KHCN)View.CurrentObject;
                detai.TrangThaiDeTai = Blazor.Common.Enums.TrangThaiDeTai.nhiemvudaketthuc;
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Đã kết thúc nghiệm vụ của đề tài này thành công!", InformationType.Success);
            };
        }
    }
    [DomainComponent]
    [XafDisplayName("Tài liệu phục vụ nghiệm thu")]
    public class TaiLieuParameter : NonPersistentObjectImpl, IDomainComponent
    {
        [XafDisplayName("Tên file")]
        public string TenFile { get; set; }


        [XafDisplayName("Tệp đính kèm ")]
        public virtual FileData File { get; set; }
    }
}
