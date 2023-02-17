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
using static DXApplication.Blazor.Common.Enums;

namespace DXApplication.Module.BusinessObjects.Main
{
    [DefaultClassOptions]
    [ImageName("manager")]
    [XafDisplayName("Cán bộ quản lý & Chủ nhiệm")]
    [DefaultProperty(nameof(HoTen))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]

    public class ChuNhiem_CanBoQuanLy : BaseObject
    {
        public ChuNhiem_CanBoQuanLy(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
        string email;
        PhanLoai phanLoai;
        FileData tep;
        string hocVi;
        string linhVucChuyenMon;
        string noiCongTac;
        string diaChi;
        string soDienThoai;
        string hoTen;
        [XafDisplayName("Họ và tên")]
        [RuleRequiredField("Bắt buộc phải có ChuNhiem_CanBoQuanLy.HoTen", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string HoTen
        {
            get => hoTen;
            set => SetPropertyValue(nameof(HoTen), ref hoTen, value);
        }
        [XafDisplayName("Số ĐT")]
        public string SoDienThoai
        {
            get => soDienThoai;
            set => SetPropertyValue(nameof(SoDienThoai), ref soDienThoai, value);
        }
        [XafDisplayName("Địa chỉ Email")]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }
        [XafDisplayName("Địa chỉ")]
        public string DiaChi
        {
            get => diaChi;
            set => SetPropertyValue(nameof(DiaChi), ref diaChi, value);
        }
        [XafDisplayName("Nơi công tác")]
        [RuleRequiredField("Bắt buộc phải có ChuNhiem_CanBoQuanLy.NoiCongTac", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string NoiCongTac
        {
            get => noiCongTac;
            set => SetPropertyValue(nameof(NoiCongTac), ref noiCongTac, value);
        }
        [XafDisplayName("Lĩnh vực chuyên môn")]
        [RuleRequiredField("Bắt buộc phải có ChuNhiem_CanBoQuanLy.LinhVucChuyenMon", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string LinhVucChuyenMon
        {
            get => linhVucChuyenMon;
            set => SetPropertyValue(nameof(LinhVucChuyenMon), ref linhVucChuyenMon, value);
        }
        [XafDisplayName("Học vị")]
        public string HocVi
        {
            get => hocVi;
            set => SetPropertyValue(nameof(HocVi), ref hocVi, value);
        }
        [XafDisplayName("Phân loại")]
        public PhanLoai PhanLoai
        {
            get => phanLoai;
            set => SetPropertyValue(nameof(PhanLoai), ref phanLoai, value);
        }
        [XafDisplayName("File đính kèm")]
        public FileData Tep
        {
            get => tep;
            set => SetPropertyValue(nameof(Tep), ref tep, value);
        }
    }
}