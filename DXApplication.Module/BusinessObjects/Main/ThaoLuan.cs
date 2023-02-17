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
    [ImageName("qa")]
    [XafDisplayName("Thảo luận")]
    [DefaultProperty(nameof(Ten))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Menu.MenuCatalog)]

    public class ThaoLuan : BaseObject
    {
        public ThaoLuan(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        FileData file;
        byte[] hinhAnh;
        Noidung_DeTai noidung_DeTai;
        string noiDung;
        string ten;
        [XafDisplayName("Tên")]
        [RuleRequiredField($"Bắt buộc phải có {nameof(ThaoLuan.Ten)}", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string Ten
        {
            get => ten;
            set => SetPropertyValue(nameof(Ten), ref ten, value);
        }
        [XafDisplayName("Nội dung")]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        [RuleRequiredField("Bắt buộc phải có ThaoLuan.NoiDung", DefaultContexts.Save, "Trường dữ liệu không được để trống")]
        public string NoiDung
        {
            get => noiDung;
            set => SetPropertyValue(nameof(NoiDung), ref noiDung, value);
        }
        [XafDisplayName("File đính kèm")]
        public FileData File
        {
            get => file;
            set => SetPropertyValue(nameof(File), ref file, value);
        }
        [XafDisplayName("Hình ảnh")]
        [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
        public byte[] HinhAnh
        {
            get => hinhAnh;
            set => SetPropertyValue(nameof(HinhAnh), ref hinhAnh, value);
        }
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [Association("Noidung_DeTai-ThaoLuans")]
        public Noidung_DeTai Noidung_DeTai
        {
            get => noidung_DeTai;
            set => SetPropertyValue(nameof(Noidung_DeTai), ref noidung_DeTai, value);
        }
    }
}