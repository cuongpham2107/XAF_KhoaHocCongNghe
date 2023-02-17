using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DXApplication.Module.BusinessObjects.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{

    public partial class PheDuyetNoiDungDeTai : ViewController
    {

        public PheDuyetNoiDungDeTai()
        {
            InitializeComponent();
            Btn_PheDuyet_NDDT();

        }
        public void Btn_PheDuyet_NDDT()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_PheDuyet_NDDT)}", "Edit")
            {
                Caption = "Phê duyệt",
                ImageName = "State_Validation_Valid",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.DetailView,
                TargetObjectType = typeof(Noidung_DeTai),
                TargetObjectsCriteria = "[TrangThaiNoiDung] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiNoiDung,chuaduocduyet#",
                ConfirmationMessage = "Xác nhận phê duyệt nội dung của đề tài này!",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            };
            action.Execute += (s, e) =>
            {
                Noidung_DeTai noidung = (Noidung_DeTai)View.CurrentObject;
                noidung.TrangThaiNoiDung = Blazor.Common.Enums.TrangThaiNoiDung.duyet;
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Phê duyệt nội dung đề tài này thành công!", InformationType.Success);
            };
        }

    }
}
