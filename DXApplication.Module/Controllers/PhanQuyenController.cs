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
using DXApplication.Blazor.BusinessObjects;
using DXApplication.Module.BusinessObjects.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PhanQuyenController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public PhanQuyenController()
        {
            InitializeComponent();
            TargetViewNesting = Nesting.Any;
            TargetViewType = ViewType.Any;
            Activated += PhanquyenController_Activated;
        }
        private void PhanquyenController_Activated(object sender, EventArgs e)
        {
            //var os = Application.CreateObjectSpace(typeof(DeTaiDuAn_KHCN));
            //var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);

            //if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers")) return;

            //if (View is ListView view)
            //{
            //    var criteria = view.CollectionSource.Criteria;


            //    if (View.ObjectTypeInfo.Type == typeof(DeTaiDuAn_KHCN))
            //        criteria.Add("crit1", new BinaryOperator("ChuNhiem_CanBoQuanLy.Oid", account.ChuNhiem_CanBoQuanLy.Oid, BinaryOperatorType.Equal));


            //}
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
