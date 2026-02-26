using System.Windows;
using System.Windows.Input;

namespace FrierenHotspot;

public partial class InstructionWindow : Window
{
    public InstructionWindow()
    {
        InitializeComponent();
        TxtHelpTitle.Text = Properties.Resources.HelpTitle;
        TxtHelpLine1.Text = Properties.Resources.HelpLine1;
        TxtHelpLine2.Text = Properties.Resources.HelpLine2;
        TxtHelpStepTitle.Text = Properties.Resources.HelpStepTitle;
        TxtHelpStep1.Text = Properties.Resources.HelpStep1;
        TxtHelpStep2.Text = Properties.Resources.HelpStep2;
        TxtHelpStep3.Text = Properties.Resources.HelpStep3;
        TxtHelpStep4.Text = Properties.Resources.HelpStep4;
        BtnOk.Content = Properties.Resources.HelpCloseBtn;
    }

    private void BtnOk_Click(object sender, RoutedEventArgs e) => Close();
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) { base.OnMouseLeftButtonDown(e); DragMove(); }
}