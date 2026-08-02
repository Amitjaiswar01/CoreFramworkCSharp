using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Automation.Test.Generator
{
	public partial class TestGenerator : Form
	{
		private const string TemplateFile = "AutomationTestTemplate.txt";
		private const string OutputFile = "{0}_{1}_Verify{2}.cs";
		private const string LP_PREFIX = "LP-";

		private const string AddTestToken = "AddTest{0}";
		private const string EndAddTestToken = "EndAddTest{0}";

		private const string DesktopJiraTaskToken = "{D_Task}";
		private const string MobileJiraTaskToken = "{M_Task}";
		private const string DesktopAdaptavistIdToken = "{D_TC}";
		private const string MobileAdaptavistIdToken = "{M_TC}";
		private const string DescriptionToken = "{MethodDescription}";
		private const string MethodNameToken = "{MethodName}";
		private const string NamespaceToken = "{Namespace}";
		private const string TestClassBaseToken = "{TestBaseClass}";
		private const string TestConfigurationToken = "{TestConfiguration}";

		public TestGenerator()
		{
			InitializeComponent();

			this.InitializeConfigurations();

			this.GenerateTestData();
		}

		private void InitializeConfigurations()
		{
			var configurations = ConfigurationManager.AppSettings["Configurations"].Split(',').ToArray();
			this.configurationsComboBox.Items.AddRange(configurations);
		}


		private void GenerateTestData()
		{
#if DEBUG
			this.desktopTaskTextBox.Text = "ACD-5055";
			this.desktopAdaptavistIdTextBox.Text = "T221";
			this.mobileTaskTextBox.Text = "ACD-5369";
			this.mobileAdaptavistIdTextBox.Text = "T455";
			this.descriptionTextBox.Text = "Verify that all items with the 'Free Shipping' attribute persist to the PDP page.";
			this.methodNametextBox.Text = "FreeShippingOnProduct";
			this.namespaceTextBox.Text = "LampsPlus.RegressionTests.Common.ProductDetail";
			this.testClassBaseTextBox.Text = "ProductDetailTestsBase";
			this.configurationsComboBox.SelectedIndex = 0;

			foreach (Control c in Controls)
			{
				if (c is TextBox)
				{
					c.ForeColor = Color.Black;
					c.Font = new Font(c.Font, FontStyle.Regular);
				}
			}
#endif
		}

		private void button1_MouseClick(object sender, MouseEventArgs e)
		{
			string errorMessage = ValidateForm();
			if (!string.IsNullOrEmpty(errorMessage))
			{
				MessageBox.Show(errorMessage);
				return;
			}

			string text = File.ReadAllText(TemplateFile);

			StringBuilder sb = new StringBuilder(text);
			sb.Replace(DesktopJiraTaskToken, this.desktopTaskTextBox.Text.Trim());
			sb.Replace(MobileJiraTaskToken, this.mobileTaskTextBox.Text.Trim());
			sb.Replace(DesktopAdaptavistIdToken, this.desktopAdaptavistIdTextBox.Text.Trim());
			sb.Replace(MobileAdaptavistIdToken, this.mobileAdaptavistIdTextBox.Text.Trim());
			sb.Replace(DescriptionToken, this.descriptionTextBox.Text.Trim());
			sb.Replace(MethodNameToken, this.methodNametextBox.Text.Trim());
			sb.Replace(NamespaceToken, this.namespaceTextBox.Text.Trim());
			sb.Replace(TestClassBaseToken, this.testClassBaseTextBox.Text.Trim());
			sb.Replace(TestConfigurationToken, this.configurationsComboBox.SelectedItem.ToString());

			UpdateTests(sb);

			var outputFile = string.Format(OutputFile, this.desktopAdaptavistIdTextBox.Text, this.mobileAdaptavistIdTextBox.Text, this.methodNametextBox.Text);

			File.WriteAllText(outputFile, sb.ToString());

			MessageBox.Show(@"File has been generated");
		}

		private string ValidateForm()
		{
			if (!windowsCheckBox.Checked && !macCheckBox.Checked && !iPadcheckBox.Checked &&
			    !iPhoneCheckBox.Checked && !androidCheckBox.Checked && !simulatorCheckBox.Checked)
				return "No checkboxes are selected";

			if (this.configurationsComboBox.SelectedIndex == -1)
				return "Test Configuration is not selected";

			if (this.desktopAdaptavistIdTextBox.Text.ToUpper().Contains(LP_PREFIX) ||
			    this.mobileAdaptavistIdTextBox.Text.ToUpper().Contains(LP_PREFIX))
				return "Adaptavist IDs should not contain 'LP_'";

			char period = this.descriptionTextBox.Text.Trim().Last();
			if ('.' != period)
				return "Description needs to end with a period";

			return ValidateTextBox();
		}

		private string ValidateTextBox()
		{
			foreach (Control c in Controls)
			{
				if (c is TextBox && string.IsNullOrEmpty(c.Text.Trim()) || c.Text.Trim().Contains("E.g."))
						return "Not all data points have been entered";
			}
			return string.Empty;
		}

		private void UpdateTests(StringBuilder text)
		{
			Decide(windowsCheckBox, text, "Windows");
			Decide(macCheckBox, text, "Mac");
			Decide(iPadcheckBox, text, "IPad");
			Decide(iPhoneCheckBox, text, "IPhone");
			Decide(androidCheckBox, text, "Android");
			Decide(simulatorCheckBox, text, "Simulator");
		}

		private void Decide(CheckBox checkBox, StringBuilder text, string type)
		{
			if (checkBox.Checked)
				IncludeTest(text, type);
			else
				ExcludeTest(text, type);
		}

		private void IncludeTest(StringBuilder sb, string type)
		{
			sb.Replace(GetTestToAddToken(type), "");
			sb.Replace(GetEndTestToAddToken(type), "\r\n\r\n");
		}

		private void ExcludeTest(StringBuilder sb, string type)
		{
			string text = sb.ToString();
			int startIndex = text.IndexOf(GetTestToAddToken(type), StringComparison.Ordinal);
			string testEnd = GetEndTestToAddToken(type);
			int endIndex = text.IndexOf(testEnd, startIndex, StringComparison.Ordinal);

			sb.Remove(startIndex, endIndex - startIndex + testEnd.Length);
		}

		private string GetTestToAddToken(string type)
		{
			string testToAdd = string.Format(AddTestToken, type);
			return $"{{{testToAdd}}}\r\n";
		}

		private string GetEndTestToAddToken(string type)
		{
			string testToEnd = string.Format(EndAddTestToken, type);
			return $"{{{testToEnd}}}\r\n";
		}

		#region Placeholder text

		private void SetPlaceHolderText(TextBox textBox, string placeHolderText)
		{
			if (string.IsNullOrEmpty(textBox.Text) || string.IsNullOrEmpty(textBox.Text.Trim()))
			{
				textBox.Text = placeHolderText;
				textBox.ForeColor = Color.Gray;
				textBox.Font = new Font(textBox.Font, FontStyle.Italic);				
			}
		}

		private void RemovePlaceHolderText(TextBox textBox)
		{
			if (textBox.Text.Contains("E.g."))
			{
				textBox.Text = string.Empty;
				textBox.ForeColor = Color.Black;
				textBox.Font = new Font(textBox.Font, FontStyle.Regular);
			}
		}

		private void desktopTaskTextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.desktopTaskTextBox);
		}

		private void desktopTaskTextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.desktopTaskTextBox, "E.g. ACD-5055");
		}

		private void desktopAdaptavistIdTextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.desktopAdaptavistIdTextBox);
		}

		private void desktopAdaptavistIdTextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.desktopAdaptavistIdTextBox, "E.g. T221");
		}

		private void mobileTaskTextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.mobileTaskTextBox);
		}

		private void mobileTaskTextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.mobileTaskTextBox, "E.g. ACD-5369");
		}

		private void mobileAdaptavistIdTextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.mobileAdaptavistIdTextBox);
		}

		private void mobileAdaptavistIdTextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.mobileAdaptavistIdTextBox, "E.g.T455");
		}

		private void descriptionTextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.descriptionTextBox);
		}

		private void descriptionTextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.descriptionTextBox, "E.g. Verify that all items with the 'Free Shipping' attribute persist to the PDP page.");
		}

		private void methodNametextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.methodNametextBox);
		}

		private void methodNametextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.methodNametextBox, "E.g. FreeShippingOnProduct");
		}

		private void namespaceTextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.namespaceTextBox);
		}

		private void namespaceTextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.namespaceTextBox, "E.g. LampsPlus.RegressionTests.Common.ProductDetail");
		}

		private void testClassBaseTextBox_Enter(object sender, EventArgs e)
		{
			RemovePlaceHolderText(this.testClassBaseTextBox);
		}

		private void testClassBaseTextBox_Leave(object sender, EventArgs e)
		{
			SetPlaceHolderText(this.testClassBaseTextBox, "E.g. ProductDetailTestsBase");
		}
		#endregion
	}
}
