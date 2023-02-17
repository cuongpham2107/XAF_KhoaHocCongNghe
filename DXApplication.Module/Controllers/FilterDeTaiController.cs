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
using DXApplication.Module.BusinessObjects.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{

    public partial class FilterDeTaiController : ObjectViewController<ListView, DeTaiDuAn_KHCN>
    {
        public FilterDeTaiController()
        {
            InitializeComponent();
            Btn_FilterDeTai_KHCN();
        }
        public void Btn_FilterDeTai_KHCN()
        {
            var action = new PopupWindowShowAction(this, $"{nameof(Btn_FilterDeTai_KHCN)}", "Edit")
            {
                Caption = "Lọc theo năm",
                ImageName = "Zoom",
                TargetViewNesting = Nesting.Root,
                TargetViewType = ViewType.ListView,
                TargetObjectType = typeof(DeTaiDuAn_KHCN),
                SelectionDependencyType = SelectionDependencyType.Independent,
            };
            action.CustomizePopupWindowParams += Btn_CustomizePopupWindowParams;
            action.Execute += Btn_TimKiemTheoNam;
        }
        private void Btn_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace os = (NonPersistentObjectSpace)e.Application.CreateObjectSpace(typeof(FilterDeTaiParameter));
            os.PopulateAdditionalObjectSpaces(Application);
            e.DialogController.SaveOnAccept = false;
            e.View = e.Application.CreateDetailView(os, os.CreateObject<FilterDeTaiParameter>());
        }
        private void Btn_TimKiemTheoNam(object s, PopupWindowShowActionExecuteEventArgs e)
        {

            var parameter = ((FilterDeTaiParameter)e.PopupWindowViewCurrentObject);

            CriteriaOperator op = null;

            if (parameter.Nam == null)
            {
                Application.ShowViewStrategy.ShowMessage("Bạn chưa nhập năm bạn muốn lọc!", InformationType.Error);
            }
            else
            {
                op = CriteriaOperator.Parse("GetYear([DenNgay]) = ?", parameter.Nam);
            }
            if (!Equals(op, null))
            {
                View.CollectionSource.Criteria["DateRange"] = op;
                Application.ShowViewStrategy.ShowMessage("Đã lọc dữ liệu theo năm thành công!", InformationType.Success);
            }
            else
            {
                View.CollectionSource.Criteria.Remove("DateRange");
            }


        }
    }

    [DomainComponent]
    [XafDisplayName("Nhập năm muốn lọc")]
    public class FilterDeTaiParameter
    {
        [XafDisplayName("Năm")]
        public int? Nam { get; set; }


    }
}
