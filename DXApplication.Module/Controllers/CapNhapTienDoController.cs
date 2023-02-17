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

    public partial class CapNhapTienDoController : ViewController
    {

        public CapNhapTienDoController()
        {
            InitializeComponent();
            Btn_CapNhapTienDo();
            Btn_DaHoanThanh();
            Btn_DuyetTrangThai();
        }
        public void Btn_DuyetTrangThai()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_DuyetTrangThai)}", "Edit")
            {
                Caption = "Duyệt tiến độ",
                ImageName = "TrackingChanges_TrackChanges",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(Noidung_DeTai),
                TargetObjectsCriteria = "[TrangThaiNoiDung] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiNoiDung,chuaduocduyet# And [TienDoThucHien] = ##Enum#DXApplication.Blazor.Common.Enums+TienDoThucHien,dahoanthanh#",
                ConfirmationMessage = "Xác nhận duyệt tiến độ này!",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
            };
            action.Execute += (s, e) =>
            {
                foreach (Noidung_DeTai item in View.SelectedObjects)
                {
                    item.TrangThaiNoiDung = Blazor.Common.Enums.TrangThaiNoiDung.duyet;
                }
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Xác nhận duyệt thành công!", InformationType.Success);
            };
        }
        public void Btn_CapNhapTienDo()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_CapNhapTienDo)}", "Edit")
            {
                Caption = "Thực hiện tiến độ",
                ImageName = "UpdateTableOfContents2",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(Noidung_DeTai),
                TargetObjectsCriteria = "[TienDoThucHien] = ##Enum#DXApplication.Blazor.Common.Enums+TienDoThucHien,luutam#",
                ConfirmationMessage = "Xác nhận đang thực hiện tiến độ này!",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
            };
            action.Execute += (s, e) =>
            {
                foreach (Noidung_DeTai item in View.SelectedObjects)
                {
                    item.TienDoThucHien = Blazor.Common.Enums.TienDoThucHien.dangthuchien;
                }
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Xác nhận đang thực hiện thành công!", InformationType.Success);
            };
        }
        public void Btn_DaHoanThanh()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_DaHoanThanh)}", "Edit")
            {
                Caption = "Hoàn thành tiến độ",
                ImageName = "ApplyChanges",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(Noidung_DeTai),
                TargetObjectsCriteria = "[TienDoThucHien] = ##Enum#DXApplication.Blazor.Common.Enums+TienDoThucHien,dangthuchien# ",
                ConfirmationMessage = "Xác nhận đã hoàn thành tiến độ này!",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
            };
            action.Execute += (s, e) =>
            {
                foreach (Noidung_DeTai item in View.SelectedObjects)
                {
                    item.TienDoThucHien = Blazor.Common.Enums.TienDoThucHien.dahoanthanh;
                }
                this.ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage("Xác nhận đã hoàn thành thành công!", InformationType.Success);
            };
        }



    }
}
