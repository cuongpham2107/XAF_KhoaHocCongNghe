using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraPrinting.Native;
using DXApplication.Blazor.Common;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static DXApplication.Blazor.Common.Enums;

namespace DXApplication.Module.BusinessObjects.Main
{
    [DefaultClassOptions]
    [ImageName("budget")]
    [XafDisplayName("Kinh phí")]
    [DefaultProperty(nameof(NoiDungChi))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]
    [CustomRootListView(AllowNew = false)]
    [CustomRootListView(FieldsToSum = new[] { "TongKinhPhi:Sum", })]

    [Appearance("HideEdit", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "[TrangThaiKinhPhi] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiKinhPhi,daduyet#", Context = "Any", Enabled = false, Priority = 0)]
    [Appearance("Trangthai", BackColor = "DeepSkyBlue", FontColor = "Black", Criteria = "[TrangThaiKinhPhi] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiKinhPhi,daduyet#", TargetItems = "TrangThaiKinhPhi", Context = "Any", Priority = 4)]
    public class KinhPhiThucHien : BaseObject
    {
        public KinhPhiThucHien(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        DateTime denNgay;
        DateTime tuNgay;
        TrangThaiKinhPhi trangThaiKinhPhi;
        DeTaiDuAn_KHCN deTaiDuAn_KHCN;
        string ghiChu;
        int tongKinhPhi;
        string noiDungChi;
        [XafDisplayName("Đề tài & dự án")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [Association("DeTaiDuAn_KHCN-KinhPhiThucHiens")]
        public DeTaiDuAn_KHCN DeTaiDuAn_KHCN
        {
            get => deTaiDuAn_KHCN;
            set => SetPropertyValue(nameof(DeTaiDuAn_KHCN), ref deTaiDuAn_KHCN, value);
        }
        [XafDisplayName("Nội dung chi")]
        [RuleRequiredField("Bắt buộc phải có KinhPhiThucHien.NoiDungChi", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string NoiDungChi
        {
            get => noiDungChi;
            set => SetPropertyValue(nameof(NoiDungChi), ref noiDungChi, value);
        }
        [XafDisplayName("Tổng kinh phí")]
        public int TongKinhPhi
        {
            get => tongKinhPhi;
            set => SetPropertyValue(nameof(TongKinhPhi), ref tongKinhPhi, value);
        }
        [XafDisplayName("Ngày thực hiện")]
        [RuleRequiredField("Bắt buộc phải có KinhPhiThucHien.TuNgay", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime TuNgay
        {
            get => tuNgay;
            set => SetPropertyValue(nameof(TuNgay), ref tuNgay, value);
        }
        [XafDisplayName("Ngày kết thúc")]
        public DateTime DenNgay
        {
            get => denNgay;
            set => SetPropertyValue(nameof(DenNgay), ref denNgay, value);
        }
        [XafDisplayName("Ghi chú")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string GhiChu
        {
            get => ghiChu;
            set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
        }
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Trạng thái")]
        public TrangThaiKinhPhi TrangThaiKinhPhi
        {
            get => trangThaiKinhPhi;
            set => SetPropertyValue(nameof(TrangThaiKinhPhi), ref trangThaiKinhPhi, value);
        }

    }
}