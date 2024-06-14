using InventoryManager.Domain;
using InventoryManager.Domain.Configuration;
using InventoryManager.Domain.Enums;
using InventoryManager.LabelPrinter;
using InventoryManager.Reports;
using Microsoft.Extensions.Options;

Console.WriteLine("Hello, World!");


StorageLocation storageLocation = new()
{
  Name = "Test case",
  SizeY  = 6,
  SizeX = 7,
};

Standard din912 = new()
{
  Name = "DIN912",
  Description = "Socket head cap screw"
};

Standard din7505 = new()
{
  Name = "DIN7075",
  Description = "Woodscrew"
};

Content screwM5X20 = new()
{
  Type = ContentType.Screw,
  Standard = din912,
  Size = "M5",
  Length = "20"
};

Content screwM5X25 = new()
{
  Type = ContentType.Screw,
  Standard = din912,
  Size = "M5",
  Length = "25"
};

Content screwM5X30 = new()
{
  Type = ContentType.Screw,
  Standard = din912,
  Size = "M5",
  Length = "30"
};

Content screwM5X35 = new()
{
  Type = ContentType.Screw,
  Standard = din912,
  Size = "M5",
  Length = "35"
};

Content screwM5X40 = new()
{
  Type = ContentType.Screw,
  Standard = din912,
  Size = "M5",
  Length = "40"
};

Content screwM5X50 = new()
{
  Type = ContentType.Screw,
  Standard = din912,
  Size = "M5",
  Length = "50"
};

Content screw5X80 = new()
{
  Type = ContentType.Screw,
  Standard = din7505,
  Size = "5,5",
  Length = "80"
};

StorageLocationContainerPosition position1 = new()
{
  PositionX = 1,
  PositionY = 1,
};

Container container1 = new()
{
  Size = ContainerSize.Size1X1, 
  Content = screwM5X40,
  Position = position1,
};

position1.Container = container1;

storageLocation.Containers.Add(position1);

StorageLocationContainerPosition position2 = new()
{
  PositionX = 7,
  PositionY = 6,
};

Container container2 = new()
{
  Size = ContainerSize.Size1X1, 
  Content = screwM5X50,
  Position = position2,
};

position2.Container = container2;

storageLocation.Containers.Add(position2);

StorageLocationContainerPosition position3 = new()
{
  PositionX = 4,
  PositionY = 3,
};

Container container3 = new()
{
  Size = ContainerSize.Size1X2, 
  Content = screw5X80,
  Position = position3,
};

position3.Container = container3;

storageLocation.Containers.Add(position3);

StorageLocationContainerPosition position4 = new()
{
  PositionX = 1,
  PositionY = 2,
};

Container container4 = new()
{
  Size = ContainerSize.Size1X1, 
  Content = screwM5X25,
  Position = position4,
};

position4.Container = container4;

storageLocation.Containers.Add(position4);

StorageLocationContainerPosition position5 = new()
{
  PositionX = 1,
  PositionY = 3,
};

Container container5 = new()
{
  Size = ContainerSize.Size1X1, 
  Content = screwM5X30,
  Position = position5,
};

position5.Container = container5;

storageLocation.Containers.Add(position5);

StorageLocationContainerPosition position6 = new()
{
  PositionX = 1,
  PositionY = 4,
};

Container container6 = new()
{
  Size = ContainerSize.Size1X1, 
  Content = screwM5X35,
  Position = position6,
};

position6.Container = container6;

storageLocation.Containers.Add(position6);


/*ReportGenerator reportGenerator = new();

using (MemoryStream caseLidSheet = reportGenerator.GenerateCaseLidSheet(storageLocation))
{
  caseLidSheet.Position = 0;

  using FileStream lidSheetPdf = new FileStream("lidsheet.pdf", FileMode.Create);
  caseLidSheet.CopyTo(lidSheetPdf);
}

using (MemoryStream labelsSheet = reportGenerator.GenerateContainerLabelsSheet(storageLocation))
{
  labelsSheet.Position = 0;
  
  using FileStream labelsPdf = new FileStream("labelsheet.pdf", FileMode.Create);
  labelsSheet.CopyTo(labelsPdf);
}*/

LabelPrinterConfiguration labelPrinterConfiguration = new()
{
  LabelPrinterEnabled = true,
  NetworkLabelPrinter = false,
  HasCutter = true,
  UsesDelayedCut = false,
  LabelPrinterAddress = Path.DirectorySeparatorChar + Path.Combine("dev", "usb", "lp3"),
};

IOptions<LabelPrinterConfiguration> options = Options.Create(new LabelPrinterConfiguration());

PrintLabel printLabel = new(options);

LabelDefinition containerLabel = new()
{
  CommandText = "^XA^FX Set print mode to cut^MMC,N^FO32,4^FB160,1,0,C,0^AAN,18,10^FD%Standard%^FS^FO32,30^FB160,1,0,C,0^ADN,36,20^FD%Content%^FS^XZ",
};

printLabel.Print(containerLabel, container1);