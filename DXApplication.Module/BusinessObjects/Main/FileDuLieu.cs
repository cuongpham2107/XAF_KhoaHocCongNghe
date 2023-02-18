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
    [ImageName("google-docs")]
    [XafDisplayName("File Dữ liệu")]
    [DefaultProperty(nameof(TenFile))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]
    [CustomRootListView(AllowNew = false)]
    public class FileDuLieu : BaseObject
    {
        public FileDuLieu(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        HoiDongThamDinh hoiDongThamDinh;
        DeTaiDuAn_KHCN deTaiDuAn_KHCN;
        FileData file;
        string tenFile;
        [XafDisplayName("Tên File")]
        public string TenFile
        {
            get => tenFile;
            set => SetPropertyValue(nameof(TenFile), ref tenFile, value);
        }
        [XafDisplayName("File đính kèm")]
        public FileData File
        {
            get => file;
            set => SetPropertyValue(nameof(File), ref file, value);
        }
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [Association("DeTaiDuAn_KHCN-FileDuLieus")]
        public DeTaiDuAn_KHCN DeTaiDuAn_KHCN
        {
            get => deTaiDuAn_KHCN;
            set => SetPropertyValue(nameof(DeTaiDuAn_KHCN), ref deTaiDuAn_KHCN, value);
        }
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [Association("HoiDongThamDinh-FileDuLieus")]
        public HoiDongThamDinh HoiDongThamDinh
        {
            get => hoiDongThamDinh;
            set => SetPropertyValue(nameof(HoiDongThamDinh), ref hoiDongThamDinh, value);
        }
    }
}