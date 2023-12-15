namespace HKW.WPF.Tests.Converters;

[TestClass]
public class ConvertersTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var end = false;
        var thread = new Thread(() =>
        {
            var window = new ConvertersTestsWindow();
            window.Show();
            var result = 1 + 2 - 3 * 4 / 5;
            window.Label_CalculatorConverterTest1_Input1.Content = 1;
            window.Label_CalculatorConverterTest1_Input2.Content = 2;
            window.Label_CalculatorConverterTest1_Input3.Content = 3;
            window.Label_CalculatorConverterTest1_Input4.Content = 4;
            window.Label_CalculatorConverterTest1_Input5.Content = 5;
            Assert.IsTrue(window.Label_CalculatorConverterTest1_Result.Content.Equals(result));
            end = true;
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join(TimeSpan.FromSeconds(10));
        Assert.IsTrue(end);
    }
}
