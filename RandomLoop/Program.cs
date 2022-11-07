using System.Security.Cryptography;
using System.Diagnostics;
using ClosedXML.Excel;


Stopwatch stopWatch = new();
RandomNumberGenerator randomGenerator = RandomNumberGenerator.Create();
Random random = new();

//Buffer for storage
int iterations = 100000;
byte[] data = new byte[4];
int[] integerArray = new int[iterations];
int[] randomIntegerArray = new int[iterations];


//Using normal Random
stopWatch.Start();
for (int i = 0; i < iterations; i++)
{
        int num = random.Next();
    randomIntegerArray[i] = num;
}
stopWatch.Stop();
TimeSpan normalRandomTime = stopWatch.Elapsed;




//Using randomNumberGenerator
stopWatch.Start();
for (int i = 0; i < iterations; i++)
{
    randomGenerator.GetBytes(data);
    //converts to int
    int value = BitConverter.ToInt32(data, 0);
    integerArray[i] = value;
}
stopWatch.Stop();
TimeSpan randomNumberGeneratorTime = stopWatch.Elapsed;




//Writing to Excel sheet
using (XLWorkbook workbook = new XLWorkbook())
{
    IXLWorksheet worksheet1 = workbook.Worksheets.Add("resultater");
    int i = 0;
    for (i = 0; i < integerArray.Length; i++)
    {
        worksheet1.Cell($"A{i + 1}").Value = integerArray[i];
        worksheet1.Cell($"E{i + 1}").Value = randomIntegerArray[i];
    }
    worksheet1.Cell($"B1").Value = "Miliseconds spend:";
    worksheet1.Cell($"B2").Value = "RandomNumberGenerator:";
    worksheet1.Cell($"C1").Value = randomNumberGeneratorTime.TotalMilliseconds; 
    worksheet1.Cell($"F1").Value = "Miliseconds spend:";
    worksheet1.Cell($"F2").Value = "Normal random:";
    worksheet1.Cell($"G1").Value = normalRandomTime.TotalMilliseconds;
    workbook.SaveAs("../../../BenchmarkResults.xlsx");
}


Console.ReadKey();


//var number = new byte[4];