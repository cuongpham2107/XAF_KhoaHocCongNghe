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
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DXApplication.Blazor.BusinessObjects;
using DXApplication.Module.BusinessObjects.Main;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DXApplication.Blazor.Common.Enums;

namespace DXApplication.Module.Controllers
{

    public partial class TuyenChonNhiemVuDeXuatController : ViewController
    {
        public TuyenChonNhiemVuDeXuatController()
        {
            InitializeComponent();
            Btn_TrangThaiDeXuat();
            Btn_TuyenChon();
        }
        public void Btn_TrangThaiDeXuat()
        {
            var action = new PopupWindowShowAction(this, $"{nameof(Btn_TrangThaiDeXuat)}", "Edit")
            {
                Caption = "Đánh giá",
                ImageName = "BO_Task",
                TargetViewNesting = Nesting.Root,
                TargetViewType = ViewType.DetailView,
                TargetObjectType = typeof(DeXuatNhiemVu_KHCN),
                TargetObjectsCriteria = "[DanhGia] = ##Enum#DXApplication.Blazor.Common.Enums+DanhGia,chuadanhgia# Or [DanhGia] = ##Enum#DXApplication.Blazor.Common.Enums+DanhGia,khongdat#",
                ConfirmationMessage = "Xác nhận Đánh giá đề xuất, nhiệm vụ này!",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            };
            action.CustomizePopupWindowParams += Btn_CustomizePopupWindowParams;
            action.Execute += Btn_DanhGia;
        }
        public void Btn_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace os = (NonPersistentObjectSpace)e.Application.CreateObjectSpace(typeof(DanhGiaParameter));
            os.PopulateAdditionalObjectSpaces(Application);
            e.DialogController.SaveOnAccept = false;
            e.View = e.Application.CreateDetailView(os, os.CreateObject<DanhGiaParameter>());
        }
        public void Btn_DanhGia(object s, PopupWindowShowActionExecuteEventArgs e)
        {

            var parameter = ((DanhGiaParameter)e.PopupWindowViewCurrentObject);

            DeXuatNhiemVu_KHCN deXuat = (DeXuatNhiemVu_KHCN)View.CurrentObject;
            deXuat.DanhGia = parameter.DanhGia;
            this.ObjectSpace.CommitChanges();
            Application.ShowViewStrategy.ShowMessage("Cập nhật Đánh giá đề xuất, nhiệm vụ thành công!", InformationType.Success);

        }
        public void Btn_TuyenChon()
        {
            var action = new SimpleAction(this, $"{nameof(Btn_TuyenChon)}", "Edit")
            {
                Caption = "Tuyển chọn",
                ImageName = "ApplyChanges",
                TargetViewNesting = Nesting.Root,
                TargetViewType = ViewType.DetailView,
                TargetObjectType = typeof(DeXuatNhiemVu_KHCN),
                TargetObjectsCriteria = "[DanhGia] = ##Enum#DXApplication.Blazor.Common.Enums+DanhGia,dat# And [DeTaiDuAn_KHCN] Is Null",
                ConfirmationMessage = "Xác nhận Tuyển chọn nhiệm vụ, đề xuất này",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            };
            action.Execute += (s, e) =>
            {
                DeXuatNhiemVu_KHCN deXuat = (DeXuatNhiemVu_KHCN)View.CurrentObject;
                deXuat.DanhGia = Blazor.Common.Enums.DanhGia.dat;
                this.ObjectSpace.CommitChanges();
                if (((DetailView)ObjectSpace.Owner).CurrentObject is DeXuatNhiemVu_KHCN dt)
                {

                    DeTaiDuAn_KHCN deTai = ObjectSpace.CreateObject<DeTaiDuAn_KHCN>();
                    var _deXuat = ObjectSpace.GetObject(dt);
                    deTai.DeXuatNhiemVu_KHCN = _deXuat;
                    deTai.TenDeTai = _deXuat.TenDeTai;
                    deTai.ThoiGianThucHien = _deXuat.ThoiGianThucHien;
                    deTai.TuNgay = _deXuat.TuNgay;
                    deTai.DenNgay = _deXuat.DenNgay;
                    deTai.ChuNhiem_CanBoQuanLy = _deXuat.ChuNhiem_CanBoQuanLy;
                    deTai.TrangThaiDeTai = Blazor.Common.Enums.TrangThaiDeTai.chuaduocduyet;
                    deTai.CapQuanLy = _deXuat.CapQuanLy;
                    deTai.LinhVucNghienCuu = _deXuat.LinhVucNghienCuu;
                    deTai.DonViDeXuat = _deXuat.DonViDeXuat;
                    deTai.File = _deXuat.File;
                    deTai.KinhTeDuKien = _deXuat.KinhPhiDuKien;
                    this.ObjectSpace.CommitChanges();
                }
                Application.ShowViewStrategy.ShowMessage("Tuyển chọn nhiệm vụ, đề xuất này thành công", InformationType.Success);
            };
        }

    }
    [DomainComponent]
    [XafDisplayName("Đánh giá đề xuất, nhiệm vụ")]
    public class DanhGiaParameter : IDomainComponent
    {
        [XafDisplayName("Trạng thái đề xuất")]
        public DanhGia DanhGia { get; set; }

    }
}
