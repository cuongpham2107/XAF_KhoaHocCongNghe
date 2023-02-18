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
    [ImageName("checklist")]
    [XafDisplayName("Đề tài, dự án")]
    [DefaultProperty(nameof(TenDeTai))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [CustomRootListView(AllowNew = false)]
    [CustomNestedListView(nameof(KinhPhiThucHiens), FieldsToSum = new[] { "TongKinhPhi:Sum" })]
    [CustomNestedListView(nameof(FileDuLieus), AllowLink = false, AllowUnlink = false)]
    [CustomNestedListView(nameof(KinhPhiThucHiens), AllowLink = false, AllowUnlink = false)]
    [CustomNestedListView(nameof(NhaKhoaHocs), AllowLink = false, AllowUnlink = false)]
    [CustomNestedListView(nameof(NoiDung_DeTais), AllowLink = false, AllowUnlink = false)]
    [CustomNestedListView(nameof(HoiDongThamDinhs), AllowUnlink = false, AllowLink = true)]

    [NavigationItem(Menu.MenuDeTai)]


    [CustomDetailView(Tabbed = true)]

    [Appearance("HideEdit", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "[TrangThaiDeTai] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,duyet# Or [TrangThaiDeTai] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,nhiemvudaketthuc# ", Context = "Any", Enabled = false, Priority = 1)]
    [Appearance("TrangThaiDeTai_1", BackColor = "DeepSkyBlue", FontColor = "Black", Criteria = "[TrangThaiDeTai] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,duyet#", TargetItems = "TrangThaiDeTai", Context = "Any", Priority = 0)]
    [Appearance("TrangThaiDeTai_3", BackColor = "OliveDrab", FontColor = "Black", Criteria = "[TrangThaiDeTai] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiDeTai,nhiemvudaketthuc#", TargetItems = "TrangThaiDeTai", Context = "Any", Priority = 0)]
    [Appearance("TrangThaiDeTai_4", FontColor = "Red", FontStyle = System.Drawing.FontStyle.Bold, Criteria = "[TrangThaiBool] = False", TargetItems = "TienDo", Context = "Any", Priority = 0)]
    [Appearance("TrangThaiDeTai_5", FontColor = "DeepSkyBlue", FontStyle = System.Drawing.FontStyle.Bold, Criteria = "[TrangThaiBool] = True", TargetItems = "TienDo", Context = "Any", Priority = 0)]

    public class DeTaiDuAn_KHCN : BaseObject
    {
        public DeTaiDuAn_KHCN(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #region Properties


        DeXuatNhiemVu_KHCN deXuatNhiemVu_KHCN;
        [XafDisplayName("Đề xuất từ")]
        [RuleRequiredField("Bắt buộc phải có DeTaiDuAn_KHCN.DeXuatNhiemVu_KHCN", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DeXuatNhiemVu_KHCN DeXuatNhiemVu_KHCN
        {
            get => deXuatNhiemVu_KHCN;
            set
            {
                if (deXuatNhiemVu_KHCN == value)
                    return;
                DeXuatNhiemVu_KHCN prevValue = deXuatNhiemVu_KHCN;
                deXuatNhiemVu_KHCN = value;
                if (IsLoading)
                    return;
                if (prevValue != null && prevValue.DeTaiDuAn_KHCN == this)
                    prevValue.DeTaiDuAn_KHCN = null;
                if (deXuatNhiemVu_KHCN != null)
                    deXuatNhiemVu_KHCN.DeTaiDuAn_KHCN = this;
                OnChanged(nameof(DeXuatNhiemVu_KHCN));
            }
        }


        string tenDeTai;
        [XafDisplayName("Tên đề tài")]
        public string TenDeTai
        {
            get => tenDeTai;
            set => SetPropertyValue(nameof(TenDeTai), ref tenDeTai, value);
        }
        string linhVucNghienCuu;
        [XafDisplayName("Lĩnh vực nghiên cứu")]
        public string LinhVucNghienCuu
        {
            get => linhVucNghienCuu;
            set => SetPropertyValue(nameof(LinhVucNghienCuu), ref linhVucNghienCuu, value);
        }
        string thoiGianThucHien;
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Thời gian thực hiện")]
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

        DateTime tuNgay;
        [XafDisplayName("Từ ngày")]
        public DateTime TuNgay
        {
            get => tuNgay;
            set => SetPropertyValue(nameof(TuNgay), ref tuNgay, value);
        }
        DateTime denNgay;
        [XafDisplayName("Đến ngày")]
        public DateTime DenNgay
        {
            get => denNgay;
            set => SetPropertyValue(nameof(DenNgay), ref denNgay, value);
        }


        int kinhTeDuKien;
        [XafDisplayName("Kinh phí dự kiến")]
        public int KinhTeDuKien
        {
            get => kinhTeDuKien;
            set => SetPropertyValue(nameof(KinhTeDuKien), ref kinhTeDuKien, value);
        }

        int kinhPhiThucTe;
        [XafDisplayName("Kinh phí thực tế")]
        [ModelDefault("AllowEdit", "False")]
        public int KinhPhiThucTe
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (KinhPhiThucHiens != null)
                    {
                        return this.KinhPhiThucHiens.Sum(i => i.TongKinhPhi);
                    }
                }
                return kinhPhiThucTe;
            }
            set => SetPropertyValue(nameof(KinhPhiThucTe), ref kinhPhiThucTe, value);
        }


        ChuNhiem_CanBoQuanLy chuNhiem_CanBoQuanLy;
        [XafDisplayName("Chủ nhiệm đề tài")]
        public ChuNhiem_CanBoQuanLy ChuNhiem_CanBoQuanLy
        {
            get => chuNhiem_CanBoQuanLy;
            set => SetPropertyValue(nameof(ChuNhiem_CanBoQuanLy), ref chuNhiem_CanBoQuanLy, value);
        }


        //HoiDongThamDinh hoiDongThamDinh;
        //[XafDisplayName("Hội đồng thẩm định")]
        ////[RuleRequiredField("Bắt buộc phải có DeTaiDuAn_KHCN.HoiDongThamDinh", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        //public HoiDongThamDinh HoiDongThamDinh
        //{
        //    get => hoiDongThamDinh;
        //    set
        //    {
        //        if (hoiDongThamDinh == value)
        //            return;
        //        HoiDongThamDinh prevValue = hoiDongThamDinh;
        //        hoiDongThamDinh = value;
        //        if (IsLoading)
        //            return;
        //        if (prevValue != null && prevValue.DeTaiDuAn_KHCN == this)
        //            prevValue.DeTaiDuAn_KHCN = null;
        //        if (hoiDongThamDinh != null)
        //            hoiDongThamDinh.DeTaiDuAn_KHCN = this;
        //        OnChanged(nameof(HoiDongThamDinh));
        //    }
        //}



        TrangThaiDeTai trangThaiDeTai;
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Trạng thái đề tài")]
        public TrangThaiDeTai TrangThaiDeTai
        {
            get
            {
                return trangThaiDeTai;
            }
            set => SetPropertyValue(nameof(TrangThaiDeTai), ref trangThaiDeTai, value);
        }


        CapQuanLy capQuanLy;
        [XafDisplayName("Cấp quản lý")]
        public CapQuanLy CapQuanLy
        {
            get => capQuanLy;
            set => SetPropertyValue(nameof(CapQuanLy), ref capQuanLy, value);
        }

        string donViDeXuat;
        [XafDisplayName("Đơn vị đề xuất")]
        public string DonViDeXuat
        {
            get => donViDeXuat;
            set => SetPropertyValue(nameof(DonViDeXuat), ref donViDeXuat, value);
        }
        FileData file;
        [XafDisplayName("File đính kèm")]
        public FileData File
        {
            get => file;
            set => SetPropertyValue(nameof(File), ref file, value);
        }
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public bool TrangThaiBool { get; set; } = false;

        string tienDo;
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Tiến độ")]
        public string TienDo
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (NoiDung_DeTais.Count() > 0)
                    {
                        var tienDoNoiDung = this.NoiDung_DeTais.All(nd => nd.TienDoThucHien == TienDoThucHien.dahoanthanh);
                        var trangThaiNoiDung = this.NoiDung_DeTais.All(tt => tt.TrangThaiNoiDung == TrangThaiNoiDung.duyet);
                        if (tienDoNoiDung == true && trangThaiNoiDung == true)
                        {
                            TrangThaiBool = true;
                            return "Đã hoàn thành hết các nội dung tiến độ!";
                        }
                        TrangThaiBool = false;
                    }
                    return "Chưa hoàn thành hoặc chưa có Nội dung & tiến độ!";
                }
                TrangThaiBool = false;
                return tienDo;
            }
            set => SetPropertyValue(nameof(TienDo), ref tienDo, value);

        }
        #endregion
        [XafDisplayName("Hội đồng thẩm định")]
        [Association("DeTaiDuAn_KHCN-HoiDongThamDinh")]
        public XPCollection<HoiDongThamDinh> HoiDongThamDinhs
        {
            get
            {
                return GetCollection<HoiDongThamDinh>(nameof(HoiDongThamDinhs));
            }
        }

        [XafDisplayName("Tài liệu phục vụ nghiệm thu")]
        [Association("DeTaiDuAn_KHCN-FileDuLieus")]
        public XPCollection<FileDuLieu> FileDuLieus
        {
            get
            {
                return GetCollection<FileDuLieu>(nameof(FileDuLieus));
            }
        }
        [XafDisplayName("Danh sách kinh phí thực hiện")]
        [Association("DeTaiDuAn_KHCN-KinhPhiThucHiens")]
        public XPCollection<KinhPhiThucHien> KinhPhiThucHiens
        {
            get
            {
                return GetCollection<KinhPhiThucHien>(nameof(KinhPhiThucHiens));
            }
        }
        [XafDisplayName("Thành viên tham gia")]
        [Association("DeTaiDuAn_KHCN-NhaKhoaHocs")]
        public XPCollection<NhaKhoaHoc> NhaKhoaHocs
        {
            get
            {
                return GetCollection<NhaKhoaHoc>(nameof(NhaKhoaHocs));
            }
        }
        [XafDisplayName("Nội dung/Tiến độ")]
        [Association("DeTaiDuAn_KHCN-NoiDung_DeTais")]
        public XPCollection<Noidung_DeTai> NoiDung_DeTais
        {
            get
            {
                return GetCollection<Noidung_DeTai>(nameof(NoiDung_DeTais));
            }
        }
    }

}