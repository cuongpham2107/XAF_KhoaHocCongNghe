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
using Microsoft.CodeAnalysis.Differencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{

    public partial class PheDuyetKinhPhi : ViewController
    {
        public PheDuyetKinhPhi()
        {
            InitializeComponent();
            Btn_DuyetKinhPhi();
            Btn_HuyDuyet();
        }
        public void Btn_DuyetKinhPhi()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_DuyetKinhPhi)}", "Edit")
            {
                Caption = "Duyệt kinh phí",
                ImageName = "Bool",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(KinhPhiThucHien),
                TargetObjectsCriteria = "[TrangThaiKinhPhi] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiKinhPhi,luutam#",
                ConfirmationMessage = "Xác nhận duyệt danh sách kinh phí thực hiện này!",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
            };
            action.Execute += (s, e) =>
            {
                foreach (KinhPhiThucHien item in View.SelectedObjects)
                {
                    item.TrangThaiKinhPhi = Blazor.Common.Enums.TrangThaiKinhPhi.daduyet;
                }
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Duyệt danh sách kinh phí này thành công!", InformationType.Success);
            };
        }
        public void Btn_HuyDuyet()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_HuyDuyet)}", "Edit")
            {
                Caption = "Huỷ duyệt",
                ImageName = "Close",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(KinhPhiThucHien),
                TargetObjectsCriteria = "[TrangThaiKinhPhi] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiKinhPhi,daduyet#",
                ConfirmationMessage = "Xác nhận huỷ duyệt danh sách kinh phí thực hiện này!",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
            };
            action.Execute += (s, e) =>
            {
                foreach (KinhPhiThucHien item in View.SelectedObjects)
                {
                    item.TrangThaiKinhPhi = Blazor.Common.Enums.TrangThaiKinhPhi.luutam;
                }
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("uỷ duyệt danh sách kinh phí này thành công!", InformationType.Success);
            };
        }

    }
}
