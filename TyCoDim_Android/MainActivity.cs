﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using TyCoDim_Android.Tabs;
using Android.Support.V4.App;
using ActionBar = Android.App.ActionBar;

namespace TyCoDim_Android
{
    [Activity(Label = "TyCoDim", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FragmentActivity
    {
        ViewPager viewPager;
        TabLayout tabLayout;
        Input Input = new Input();
        Graphic Graphic = new Graphic();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitleTextColor(Android.Graphics.Color.White);
            SetActionBar(toolbar);

            ActionBar.Title = "TyCoDim";

            viewPager = (ViewPager)FindViewById(Resource.Id.pager);
            tabLayout = (TabLayout)FindViewById(Resource.Id.tabs);
            SetupViewPager(viewPager);
            tabLayout.SetupWithViewPager(viewPager, true);

            viewPager.PageSelected += ViewPager_PageSelected;
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if (e.Position == 1)
            {
                //In your graphic fragment, hide the keyboard.
                var im = ((InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService));

                if (CurrentFocus != null)
                {
                    im.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.None);
                }
            }
        }

        public override void OnBackPressed()
        {
            viewPager.SetCurrentItem(0, true);
        }

        private void SetupViewPager(ViewPager Pager)
        {
            Pager.OffscreenPageLimit = 2;

            PageAdapter adapter = new PageAdapter(SupportFragmentManager);
            adapter.AddFragment(Input, "");
            adapter.AddFragment(Graphic, "");

            Pager.Adapter = adapter;
        }
    }
}

