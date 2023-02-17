using DevExpress.ExpressApp.DC;

namespace DXApplication.Blazor.Common;

public class Enums
{
    public enum CapQuanLy
    {
        [XafDisplayName("Cấp Bộ")] capbo = 0,
        [XafDisplayName("Cấp Tỉnh")] captinh = 1,
        [XafDisplayName("Cấp Huyện")] caphuyen = 2,

    }
    public enum DanhGia
    {
        [XafDisplayName("Chưa đánh giá")] chuadanhgia = 0,
        [XafDisplayName("Đạt")] dat = 1,
        [XafDisplayName("Không đạt")] khongdat = 2,
    }

    public enum TrangThaiDeTai
    {
        [XafDisplayName("Đã duyệt")] duyet = 1,
        [XafDisplayName("Chưa được duyệt")] chuaduocduyet = 0,
        [XafDisplayName("Nhiệm vụ đã kết thúc")] nhiemvudaketthuc = 2,
    }
    public enum PhanLoai
    {
        [XafDisplayName("Chủ nhiệm đề tài")] duyet = 1,
        [XafDisplayName("Cán bộ quản lý")] chuaduocduyet = 0,
    }
    public enum TrangThaiKinhPhi
    {
        [XafDisplayName("Đã duyệt")] daduyet = 1,
        [XafDisplayName("Lưu tạm")] luutam = 0,
    }
    public enum TrangThaiGopY
    {
        [XafDisplayName("Đã sửa theo góp ý")] dasua = 0,
        [XafDisplayName("Chưa sửa góp ý")] chuasua = 1,
    }
    public enum TrangThaiNoiDung
    {
        [XafDisplayName("Đã duyệt")] duyet = 1,
        [XafDisplayName("Chưa được duyệt")] chuaduocduyet = 0,
    }
    public enum TienDoThucHien
    {
        [XafDisplayName("Lưu tạm")] luutam = 0,
        [XafDisplayName("Đang thực hiện")] dangthuchien = 1,
        [XafDisplayName("Đã hoàn thành")] dahoanthanh = 2,

    }

}