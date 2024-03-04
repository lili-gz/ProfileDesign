//// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.common;
using ConsoleApp1.pojo;
using ConsoleApp1.utils;

BaseContext.groundLine = BaseFileOpt.ReadCsvFile("C:\\Users\\lilili\\Documents\\Tencent Files\\2785782829\\FileRecv\\纵断面地面线数据\\匝道-A中桩地面线.csv");

BaseContext.GenerateContinueGroundLine();

BaseDesignLine baseDesignLine = new BaseDesignLine();
baseDesignLine.designLine.Add(BaseContext.continueGroundLine[0].Clone());
baseDesignLine.designLine.Add(BaseContext.continueGroundLine[BaseContext.continueGroundLine.Length - 1].Clone());
baseDesignLine.UpdateWorkbench();

foreach (var item in baseDesignLine.workbench)
{
    if (item.intersectionPoint == true)
    {
        Console.WriteLine(item.mileage + " " + item.elevation);
    }
}




