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
    [ImageName("rising")]
    [XafDisplayName("Tiến độ & Nội dung")]
    [DefaultProperty(nameof(NoiDung))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]

    [CustomNestedListView(nameof(ThaoLuans), AllowLink = false, AllowUnlink = false)]

    [Appearance("HideEdit", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "[TrangThaiNoiDung] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiNoiDung,duyet#", Context = "Any", Enabled = false, Priority = 0)]
    [Appearance("TienDoThucHien_1", BackColor = "DeepSkyBlue", FontColor = "Black", Criteria = "[TienDoThucHien] = ##Enum#DXApplication.Blazor.Common.Enums+TienDoThucHien,dahoanthanh#", TargetItems = "TienDoThucHien", Context = "Any", Priority = 3)]
    [Appearance("TienDoThucHien_2", BackColor = "Gold", FontColor = "Black", Criteria = "[TienDoThucHien] = ##Enum#DXApplication.Blazor.Common.Enums+TienDoThucHien,dangthuchien#", TargetItems = "TienDoThucHien", Context = "Any", Priority = 4)]
    [Appearance("Trangthai", BackColor = "DeepSkyBlue", FontColor = "Black", Criteria = "[TrangThaiNoiDung] = ##Enum#DXApplication.Blazor.Common.Enums+TrangThaiNoiDung,duyet#", TargetItems = "TrangThaiNoiDung", Context = "Any", Priority = 5)]
    public class Noidung_DeTai : BaseObject
    {
        public Noidung_DeTai(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        string sanPhamDatDuoc;
        FileData file;
        DeTaiDuAn_KHCN deTaiDuAn_KHCN;
        string ghiChu;
        DateTime thoiGianDuKien;
        TienDoThucHien tienDoThucHien;
        DateTime ngayTao;
        TrangThaiNoiDung trangThaiNoiDung;
        string noiDung;
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        [XafDisplayName("Nội dung & Tiến độ")]
        [RuleRequiredField("Bắt buộc phải có Noidung_DeTai.NoiDung", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string NoiDung
        {
            get => noiDung;
            set => SetPropertyValue(nameof(NoiDung), ref noiDung, value);
        }
        [XafDisplayName("Ngày tạo")]
        [RuleRequiredField("Bắt buộc phải có Noidung_DeTai.NgayTao", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public DateTime NgayTao
        {
            get => ngayTao;
            set => SetPropertyValue(nameof(NgayTao), ref ngayTao, value);
        }
        [XafDisplayName("Dự kiến hoàn thành")]

        public DateTime ThoiGianDuKien
        {
            get => thoiGianDuKien;
            set => SetPropertyValue(nameof(ThoiGianDuKien), ref thoiGianDuKien, value);
        }
        [XafDisplayName("Tiến độ thực hiện")]
        [ModelDefault("AllowEdit", "False")]
        public TienDoThucHien TienDoThucHien
        {
            get => tienDoThucHien;
            set => SetPropertyValue(nameof(TienDoThucHien), ref tienDoThucHien, value);
        }
        [XafDisplayName("Trạng thái")]
        [ModelDefault("AllowEdit", "False")]
        public TrangThaiNoiDung TrangThaiNoiDung
        {
            get => trangThaiNoiDung;
            set => SetPropertyValue(nameof(TrangThaiNoiDung), ref trangThaiNoiDung, value);
        }
        [XafDisplayName("Sản phẩm đạt được")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string SanPhamDatDuoc
        {
            get => sanPhamDatDuoc;
            set => SetPropertyValue(nameof(SanPhamDatDuoc), ref sanPhamDatDuoc, value);
        }
        [XafDisplayName("Ghi chú")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string GhiChu
        {
            get => ghiChu;
            set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
        }

        [XafDisplayName("File đính kèm")]
        public FileData File
        {
            get => file;
            set => SetPropertyValue(nameof(File), ref file, value);
        }
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [XafDisplayName("Đề tài & dự án")]
        [Association("DeTaiDuAn_KHCN-NoiDung_DeTais")]
        public DeTaiDuAn_KHCN DeTaiDuAn_KHCN
        {
            get => deTaiDuAn_KHCN;
            set => SetPropertyValue(nameof(DeTaiDuAn_KHCN), ref deTaiDuAn_KHCN, value);
        }
        [XafDisplayName("Thảo luận")]
        [Association("Noidung_DeTai-ThaoLuans")]
        public XPCollection<ThaoLuan> ThaoLuans
        {
            get
            {
                return GetCollection<ThaoLuan>(nameof(ThaoLuans));
            }
        }

    }
}