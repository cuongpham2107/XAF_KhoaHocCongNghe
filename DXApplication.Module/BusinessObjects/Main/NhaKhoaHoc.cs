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
    [ImageName("data-scientist")]
    [XafDisplayName("Nhà khoa học")]
    [DefaultProperty(nameof(HoTen))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]
    [CustomRootListView(AllowNew = false)]

    public class NhaKhoaHoc : BaseObject
    {
        public NhaKhoaHoc(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        DeTaiDuAn_KHCN deTaiDuAn_KHCN;
        FileData tep;
        string hocVi;
        string linhVucChuyenMon;
        string noiCongTac;
        string diaChi;
        string soDienThoai;
        string hoTen;
        [XafDisplayName("Họ và tên")]
        [RuleRequiredField("Bắt buộc phải có NhaKhoaHoc.HoTen", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string HoTen
        {
            get => hoTen;
            set => SetPropertyValue(nameof(HoTen), ref hoTen, value);
        }
        [XafDisplayName("Số điện thoại")]
        public string SoDienThoai
        {
            get => soDienThoai;
            set => SetPropertyValue(nameof(SoDienThoai), ref soDienThoai, value);
        }
        [XafDisplayName("Địa chỉ")]
        public string DiaChi
        {
            get => diaChi;
            set => SetPropertyValue(nameof(DiaChi), ref diaChi, value);
        }
        [XafDisplayName("Nơi công tác")]
        public string NoiCongTac
        {
            get => noiCongTac;
            set => SetPropertyValue(nameof(NoiCongTac), ref noiCongTac, value);
        }
        [XafDisplayName("Lĩnh vực chuyên môn")]
        [RuleRequiredField("Bắt buộc phải có NhaKhoaHoc.LinhVucChuyenMon", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string LinhVucChuyenMon
        {
            get => linhVucChuyenMon;
            set => SetPropertyValue(nameof(LinhVucChuyenMon), ref linhVucChuyenMon, value);
        }
        [XafDisplayName("Học vị")]
        [RuleRequiredField("Bắt buộc phải có NhaKhoaHoc.HocVi", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string HocVi
        {
            get => hocVi;
            set => SetPropertyValue(nameof(HocVi), ref hocVi, value);
        }
        [XafDisplayName("File đính kèm")]
        public FileData Tep
        {
            get => tep;
            set => SetPropertyValue(nameof(Tep), ref tep, value);
        }
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [XafDisplayName("Đề tài & dự án")]
        [Association("DeTaiDuAn_KHCN-NhaKhoaHocs")]
        public DeTaiDuAn_KHCN DeTaiDuAn_KHCN
        {
            get => deTaiDuAn_KHCN;
            set => SetPropertyValue(nameof(DeTaiDuAn_KHCN), ref deTaiDuAn_KHCN, value);
        }
    }
}