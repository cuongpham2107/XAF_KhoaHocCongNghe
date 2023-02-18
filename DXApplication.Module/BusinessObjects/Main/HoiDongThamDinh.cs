using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DXApplication.Blazor.Common;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DXApplication.Module.BusinessObjects.Main
{
    [DefaultClassOptions]
    [ImageName("meeting")]
    [XafDisplayName("Hội đồng thẩm định")]
    [DefaultProperty(nameof(TenHoiDong))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]

    [CustomNestedListView(nameof(FileDuLieus), AllowLink = false, AllowUnlink = false)]
    [CustomNestedListView(nameof(ThanhVienHoiDongTDs), AllowLink = false, AllowUnlink = false)]
    [CustomNestedListView(nameof(DeTaiDuAn_KHCNs), AllowUnlink = false, AllowLink = true)]



    public class HoiDongThamDinh : BaseObject
    {
        public HoiDongThamDinh(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        string ghiChu;
        string noiDungThanhLap;
        string chuTichHoiDong;
        DateTime ngayThanhLap;
        string tenHoiDong;
        [XafDisplayName("Tên hội đồng")]
        [RuleRequiredField("Bắt buộc phải có HoiDongThamDinh.TenHoiDong", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string TenHoiDong
        {
            get => tenHoiDong;
            set => SetPropertyValue(nameof(TenHoiDong), ref tenHoiDong, value);
        }
        [XafDisplayName("Ngày thành lập")]
        [RuleRequiredField("Bắt buộc phải có HoiDongThamDinh.NgayThanhLap", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime NgayThanhLap
        {
            get => ngayThanhLap;
            set => SetPropertyValue(nameof(NgayThanhLap), ref ngayThanhLap, value);
        }
        [XafDisplayName("Chủ tịch hội đồng")]
        [RuleRequiredField("Bắt buộc phải có HoiDongThamDinh.ChuTichHoiDong", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string ChuTichHoiDong
        {
            get => chuTichHoiDong;
            set => SetPropertyValue(nameof(ChuTichHoiDong), ref chuTichHoiDong, value);
        }
        [XafDisplayName("Nội dung thành lập")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string NoiDungThanhLap
        {
            get => noiDungThanhLap;
            set => SetPropertyValue(nameof(NoiDungThanhLap), ref noiDungThanhLap, value);
        }
        [XafDisplayName("Ghi chú")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string GhiChu
        {
            get => ghiChu;
            set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
        }


        [XafDisplayName("Danh sách File đính kèm")]
        [Association("HoiDongThamDinh-FileDuLieus")]
        public XPCollection<FileDuLieu> FileDuLieus
        {
            get
            {
                return GetCollection<FileDuLieu>(nameof(FileDuLieus));
            }
        }
        [XafDisplayName("Danh sách thành viên trong hội đồng")]
        [Association("HoiDongThamDinh-ThanhVienHoiDongTDs")]
        public XPCollection<ThanhVienHoiDongTD> ThanhVienHoiDongTDs
        {
            get
            {
                return GetCollection<ThanhVienHoiDongTD>(nameof(ThanhVienHoiDongTDs));
            }
        }
        [XafDisplayName("Đề tài & dự án")]
        [Association("DeTaiDuAn_KHCN-HoiDongThamDinh")]
        public XPCollection<DeTaiDuAn_KHCN> DeTaiDuAn_KHCNs
        {
            get
            {
                return GetCollection<DeTaiDuAn_KHCN>(nameof(DeTaiDuAn_KHCNs));
            }
        }
    }
}