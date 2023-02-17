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

    public partial class SuaGopYController : ViewController
    {

        public SuaGopYController()
        {
            InitializeComponent();
            Btn_SuaGopY();
        }
        public void Btn_SuaGopY()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_SuaGopY)}", "Edit")
            {
                Caption = "Đã hoàn thành",
                ImageName = "State_Validation_Valid",
                TargetViewNesting = Nesting.Nested,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(ThanhVienHoiDongTD),
                TargetObjectsCriteria = "[TrangThaiGopY] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiGopY,chuasua#",
                ConfirmationMessage = "Xác nhận đã sửa Đề tài, dự án thep góp ý!",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
            };
            action.Execute += (s, e) =>
                {

                    foreach (ThanhVienHoiDongTD item in View.SelectedObjects)
                    {
                        item.TrangThaiGopY = Blazor.Common.Enums.TrangThaiGopY.dasua;
                    }
                    this.ObjectSpace.CommitChanges();
                    Application.ShowViewStrategy.ShowMessage("Cập nhập trạng thái thành công!", InformationType.Success);
                };
        }
    }
}
