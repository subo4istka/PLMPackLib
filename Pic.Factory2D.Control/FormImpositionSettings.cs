﻿#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pic.Factory2D;

using log4net;
#endregion

namespace Pic.Factory2D.Control
{
    public partial class FormImpositionSettings : Form
    {
        #region Data members
        private CardboardFormat _cardboardFormat;
        private CardboardFormatLoader _formatLoader;
        protected static readonly ILog _log = LogManager.GetLogger(typeof(FormImpositionSettings));
        #endregion

        #region Constructor
        public FormImpositionSettings()
        {
            InitializeComponent();
            // initialize format loader with default values
            FormatLoader = new CardboardFormatLoaderDefault();
        }
        #endregion

        #region Public properties
        public CardboardFormatLoader FormatLoader
        {
            set
            {
                _formatLoader = value;
                _cardboardFormat = null;
            }
        }

        public ImpositionTool.HAlignment HorizontalAlignment
        {
            get { return (ImpositionTool.HAlignment) cbRightLeft.SelectedIndex; }
        }

        public ImpositionTool.VAlignment VerticalAlignment
        {
            get { return (ImpositionTool.VAlignment) cbTopBottom.SelectedIndex; }
        }
        #endregion

        #region Private methods
        private void FillCombo()
        { 
            // fill combo
            cbCardboardFormat.Items.Clear();
            if (null == _formatLoader)
            {
                _log.Debug("Format loader is not set! Can not retrieve any cardboard formats!");
                return;
            }
            cbCardboardFormat.Items.AddRange(_formatLoader.LoadCardboardFormats());
            // select first item
            int selectedIndex = Math.Min(Properties.Settings.Default.ImpositionCarboardFormat, cbCardboardFormat.Items.Count - 1);
            if (cbCardboardFormat.Items.Count > selectedIndex)
            {
                cbCardboardFormat.SelectedIndex = selectedIndex;
                OnCardboardFormatChanged();
            }
            // enable OK button
            bnOk.Enabled = (null != _cardboardFormat);
        }
        #endregion

        #region Public properties
        public double ImpMarginBottomTop
        { get { return (double)nudTopBottomMargin.Value; } }
        public double ImpMarginLeftRight
        { get { return (double)nudLeftRightMargin.Value; } }
        public double ImpRemainingMarginBottomTop
        { get { return (double)nudTopBottomRemaining.Value; } }
        public double ImpRemainingMarginLeftRight
        { get { return (double)nudLeftRightRemaining.Value; } }
        public double ImpSpaceBetween
        { get { return (double)nudSpaceBetween.Value; } }
        public double OffsetX
        {
            get { return (double)nudOffsetX.Value; }
            set { nudOffsetX.Value = (decimal)value; }
        }
        public double OffsetY
        {
            get { return (double)nudOffsetY.Value; }
            set { nudOffsetY.Value = (decimal)value; }
        }
        public bool AllowRowRotation { get { return cbAllowRowRotation.Checked; } }
        public bool AllowColumnRotation { get { return cbAllowColumnRotation.Checked; } }
        public CardboardFormat CardboardFormat { get { return _cardboardFormat; } }
        #endregion

        #region Private methods
        private void OnCardboardFormatChanged()
        {
            _cardboardFormat = cbCardboardFormat.SelectedItem as CardboardFormat;
        }
        #endregion

        #region Event handlers
        private void cbCardboardFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCardboardFormatChanged();
        }

        private void bnEditCardboardFormats_Click(object sender, EventArgs e)
        {
            try
            {
                _formatLoader.EditCardboardFormats();
                FillCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormImpositionSettings_Load(object sender, EventArgs e)
        {
            try
            {
                FillCombo();
                // initialize FormatLoader
                if (cbCardboardFormat.Items.Count == 0)
                {
                    _formatLoader.EditCardboardFormats();
                    FillCombo();
                }
                // initialize margins
                cbTopBottom.SelectedIndex = Properties.Settings.Default.ComboTopBottomIndex;
                cbRightLeft.SelectedIndex = Properties.Settings.Default.ComboLeftRightIndex;
                nudTopBottomMargin.Value = Properties.Settings.Default.ImpositionMarginTopBottom;
                nudTopBottomRemaining.Value = Properties.Settings.Default.ImpositionRemainingMarginTopBottom;
                nudLeftRightMargin.Value = Properties.Settings.Default.ImpositionMarginLeftRight;
                nudLeftRightRemaining.Value = Properties.Settings.Default.ImpositionRemainingMarginLeftRight;
                nudSpaceBetween.Value = Properties.Settings.Default.ImpositionSpaceBetween;

                // update combo results
                cbPlacement_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormImpositionSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // cardboard format
            Properties.Settings.Default.ImpositionCarboardFormat = cbCardboardFormat.SelectedIndex;
            // margins
            Properties.Settings.Default.ComboTopBottomIndex = cbTopBottom.SelectedIndex;
            Properties.Settings.Default.ComboLeftRightIndex = cbRightLeft.SelectedIndex;
            Properties.Settings.Default.ImpositionMarginTopBottom = nudTopBottomMargin.Value;
            Properties.Settings.Default.ImpositionRemainingMarginTopBottom = nudTopBottomRemaining.Value;
            Properties.Settings.Default.ImpositionMarginLeftRight = nudLeftRightMargin.Value;
            Properties.Settings.Default.ImpositionRemainingMarginLeftRight = nudLeftRightRemaining.Value;
            Properties.Settings.Default.ImpositionSpaceBetween = nudSpaceBetween.Value;
        }
        
        private void cbPlacement_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cbTopBottom
            switch (cbTopBottom.SelectedIndex)
            {
                case 0: lbRemainingBottomTop.Text = Properties.Resources.ID_MINMARGINTOP; break;
                case 1: lbRemainingBottomTop.Text = Properties.Resources.ID_MINMARGINBOTTOM; break;
                default: break;
            }
            lbRemainingBottomTop.Visible = cbTopBottom.SelectedIndex != 2;
            nudTopBottomRemaining.Visible = cbTopBottom.SelectedIndex != 2;
            lbmm2.Visible = cbTopBottom.SelectedIndex != 2;

            // cbRightLeft
            switch (cbRightLeft.SelectedIndex)
            {
                case 0: lbRemainingLeftRight.Text = Properties.Resources.ID_MINMARGINRIGHT; break;
                case 1: lbRemainingLeftRight.Text = Properties.Resources.ID_MINMARGINLEFT; break;
                default: break;
            }
            lbRemainingLeftRight.Visible = cbRightLeft.SelectedIndex != 2;
            nudLeftRightRemaining.Visible = cbRightLeft.SelectedIndex != 2;
            lbmm4.Visible = cbRightLeft.SelectedIndex != 2;
        }
        #endregion
    }
}