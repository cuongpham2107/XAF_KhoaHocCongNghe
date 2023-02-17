using DevExpress.CodeParser;
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
using DevExpress.XtraRichEdit.Model;
using DXApplication.Blazor.Common;
using DXApplication.Module.Extension;
using System.ComponentModel;
using static DXApplication.Blazor.Common.Enums;

namespace DXApplication.Module.BusinessObjects.Main
{
    [DefaultClassOptions]
    [ImageName("proposal")]
    [XafDisplayName("Đề xuất, nhiệm vụ")]
    [DefaultProperty(nameof(TenDeTai))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuDeXuat)]

    [Appearance("TrangThaiDeXuat_1", BackColor = "DeepSkyBlue", FontColor = "Black", Criteria = "[DanhGia] = ##Enum#DXApplication.Blazor.Common.Enums+DanhGia,dat#", TargetItems = "DanhGia", Context = "Any", Priority = 0)]
    [Appearance("TrangThaiDeXuat_2", BackColor = "Red", FontColor = "Black", Criteria = "[DanhGia] = ##Enum#DXApplication.Blazor.Common.Enums+DanhGia,khongdat#", TargetItems = "DanhGia", Context = "Any", Priority = 0)]
    public class DeXuatNhiemVu_KHCN : BaseObject
    {
        public DeXuatNhiemVu_KHCN(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }


        string donViDeXuat;
        DanhGia danhGia;
        CapQuanLy capQuanLy;
        FileData file;
        ChuNhiem_CanBoQuanLy chuNhiem_CanBoQuanLy;
        int kinhPhiDuKien;
        string ketQuaDuKien;
        string tenSanPham;
        string noiDungChinh;
        string mucTieu;
        string tinhCapThiet_DT;
        string linhVucNghienCuu;
        string tenDeTai;

        [XafDisplayName("Tên đề tài")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.TenDeTai", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string TenDeTai
        {
            get => tenDeTai;
            set => SetPropertyValue(nameof(TenDeTai), ref tenDeTai, value);
        }
        [XafDisplayName("Chủ nhiệm đề tài")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.ChuNhiem_CanBoQuanLy", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public ChuNhiem_CanBoQuanLy ChuNhiem_CanBoQuanLy
        {
            get => chuNhiem_CanBoQuanLy;
            set => SetPropertyValue(nameof(ChuNhiem_CanBoQuanLy), ref chuNhiem_CanBoQuanLy, value);
        }
        [XafDisplayName("Lĩnh vực nghiên cứu")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.LinhVucNghienCuu", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string LinhVucNghienCuu
        {
            get => linhVucNghienCuu;
            set => SetPropertyValue(nameof(LinhVucNghienCuu), ref linhVucNghienCuu, value);
        }
        [XafDisplayName("Tính cấp thiết")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.TinhCapThiet_DT", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string TinhCapThiet_DT
        {
            get => tinhCapThiet_DT;
            set => SetPropertyValue(nameof(TinhCapThiet_DT), ref tinhCapThiet_DT, value);
        }
        [XafDisplayName("Mục tiêu")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.MucTieu", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string MucTieu
        {
            get => mucTieu;
            set => SetPropertyValue(nameof(MucTieu), ref mucTieu, value);
        }
        [XafDisplayName("Nội dung chính")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.NoiDungChinh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string NoiDungChinh
        {
            get => noiDungChinh;
            set => SetPropertyValue(nameof(NoiDungChinh), ref noiDungChinh, value);
        }
        [XafDisplayName("Tên sản phẩm")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.TenSanPham", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string TenSanPham
        {
            get => tenSanPham;
            set => SetPropertyValue(nameof(TenSanPham), ref tenSanPham, value);
        }
        [XafDisplayName("Hiệu quả dự kiến")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.KetQuaDuKien", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string KetQuaDuKien
        {
            get => ketQuaDuKien;
            set => SetPropertyValue(nameof(KetQuaDuKien), ref ketQuaDuKien, value);
        }


        private DateTime tuNgay;
        [XafDisplayName("Từ ngày")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.TuNgay", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime TuNgay
        {
            get => tuNgay;
            set => SetPropertyValue(nameof(TuNgay), ref tuNgay, value);
        }

        private DateTime denNgay;
        [XafDisplayName("Đến ngày")]
        [RuleRequiredField("Bắt buộc phải có DeXuatNhiemVu_KHCN.DenNgay", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime DenNgay
        {
            get => denNgay;
            set => SetPropertyValue(nameof(DenNgay), ref denNgay, value);
        }

        private string thoiGianThucHien;
        [XafDisplayName("Thời gian thực hiện")]
        [ModelDefault("AllowEdit", "False")]
        public string ThoiGianThucHien
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    int nbMonths = ((DenNgay.Year - TuNgay.Year) * 12) + DenNgay.Month - TuNgay.Month;
                    return $"{nbMonths} tháng";
                }
                return thoiGianThucHien;

            }
            set => SetPropertyValue(nameof(ThoiGianThucHien), ref thoiGianThucHien, value);
        }
        [XafDisplayName("Kinh phí dự kiến")]
        public int KinhPhiDuKien
        {
            get => kinhPhiDuKien;
            set => SetPropertyValue(nameof(KinhPhiDuKien), ref kinhPhiDuKien, value);
        }
        [XafDisplayName("Cấp quản lý")]

        public CapQuanLy CapQuanLy
        {
            get => capQuanLy;
            set => SetPropertyValue(nameof(CapQuanLy), ref capQuanLy, value);
        }
        [XafDisplayName("Đơn vị đề xuất")]
        [RuleRequiredField($"Bắt buộc phải có {nameof(DeXuatNhiemVu_KHCN.DonViDeXuat)}", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string DonViDeXuat
        {
            get => donViDeXuat;
            set => SetPropertyValue(nameof(DonViDeXuat), ref donViDeXuat, value);
        }
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Trạng thái đề xuất")]
        public DanhGia DanhGia
        {
            get => danhGia;
            set => SetPropertyValue(nameof(DanhGia), ref danhGia, value);
        }
        [XafDisplayName("File đính kèm")]
        public FileData File
        {
            get => file;
            set => SetPropertyValue(nameof(File), ref file, value);
        }
        private DeTaiDuAn_KHCN _deTaiDuAn_KHCN;
        [XafDisplayName("Thuộc đề tài & dự án")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("AlowEdit", "False")]
        public DeTaiDuAn_KHCN DeTaiDuAn_KHCN
        {
            get { return _deTaiDuAn_KHCN; }
            set
            {
                if (_deTaiDuAn_KHCN == value)
                    return;
                DeTaiDuAn_KHCN prevValue = _deTaiDuAn_KHCN;
                _deTaiDuAn_KHCN = value;
                if (IsLoading) return;
                if (prevValue != null && prevValue.DeXuatNhiemVu_KHCN == this) prevValue.DeXuatNhiemVu_KHCN = null;
                if (_deTaiDuAn_KHCN != null) _deTaiDuAn_KHCN.DeXuatNhiemVu_KHCN = this;
                OnChanged(nameof(DeTaiDuAn_KHCN));
            }
        }


    }
}