using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using DXApplication.Module.BusinessObjects.Main;
using System.ComponentModel;
using System.Text;

namespace DXApplication.Blazor.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo
{
    public ApplicationUser(Session session) : base(session) { }

    [Browsable(false)]
    [Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo
    {
        get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
    }

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
    {
        ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
    [DevExpress.ExpressApp.DC.XafDisplayName("Chủ nhiệm - Cán bộ quản lý")]
    ChuNhiem_CanBoQuanLy chuNhiem_CanBoQuanLy;

    public ChuNhiem_CanBoQuanLy ChuNhiem_CanBoQuanLy
    {
        get => chuNhiem_CanBoQuanLy;
        set => SetPropertyValue(nameof(ChuNhiem_CanBoQuanLy), ref chuNhiem_CanBoQuanLy, value);
    }
}
