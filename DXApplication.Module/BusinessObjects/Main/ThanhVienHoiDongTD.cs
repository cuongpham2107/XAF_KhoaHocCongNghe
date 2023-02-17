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
    [ImageName("team")]
    [XafDisplayName("Thành viên hội đồng")]
    [DefaultProperty(nameof(TenThanhVien))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]



    [Appearance("TrangThaiGopY_1", BackColor = "Red", FontColor = "Black", Criteria = "[TrangThaiGopY] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiGopY,chuasua#", TargetItems = "TrangThaiGopY", Context = "Any", Priority = 3)]
    [Appearance("TrangThaiGopY_2", BackColor = "DeepSkyBlue", FontColor = "Black", Criteria = "[TrangThaiGopY] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiGopY,dasua#", TargetItems = "TrangThaiGopY", Context = "Any", Priority = 4)]
    public class ThanhVienHoiDongTD : BaseObject
    {
        public ThanhVienHoiDongTD(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        TrangThaiGopY trangThaiGopY;
        HoiDongThamDinh hoiDongThamDinh;
        string gopY;
        string diaChi;
        string email;
        string soDienThoai;
        string tenThanhVien;
        [XafDisplayName("Tên thành viên")]
        [RuleRequiredField("Bắt buộc phải có ThanhVienHoiDongTD.TenThanhVien", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string TenThanhVien
        {
            get => tenThanhVien;
            set => SetPropertyValue(nameof(TenThanhVien), ref tenThanhVien, value);
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
        [XafDisplayName("Góp ý")]
        [RuleRequiredField("Bắt buộc phải có ThanhVienHoiDongTD.GopY", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string GopY
        {
            get => gopY;
            set => SetPropertyValue(nameof(GopY), ref gopY, value);
        }
        [XafDisplayName("Trạng thái góp ý")]
        public TrangThaiGopY TrangThaiGopY
        {
            get => trangThaiGopY;
            set => SetPropertyValue(nameof(TrangThaiGopY), ref trangThaiGopY, value);
        }
        [VisibleInDetailView(true)]
        [VisibleInListView(true)]
        [XafDisplayName("Thuộc hội đồng thảm định")]
        [Association("HoiDongThamDinh-ThanhVienHoiDongTDs")]
        public HoiDongThamDinh HoiDongThamDinh
        {
            get => hoiDongThamDinh;
            set => SetPropertyValue(nameof(HoiDongThamDinh), ref hoiDongThamDinh, value);
        }

    }
}