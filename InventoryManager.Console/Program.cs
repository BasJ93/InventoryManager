using InventoryManager.Domain;
using InventoryManager.Domain.Enums;
using InventoryManager.Reports;

Console.WriteLine("Hello, World!");


StorageCase storageCase = new()
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

CaseContainerPosition position1 = new()
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

storageCase.Containers.Add(position1);

CaseContainerPosition position2 = new()
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

storageCase.Containers.Add(position2);

CaseContainerPosition position3 = new()
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

storageCase.Containers.Add(position3);

CaseContainerPosition position4 = new()
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

storageCase.Containers.Add(position4);

CaseContainerPosition position5 = new()
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

storageCase.Containers.Add(position5);

CaseContainerPosition position6 = new()
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

storageCase.Containers.Add(position6);


ReportGenerator reportGenerator = new();

using (MemoryStream caseLidSheet = reportGenerator.GenerateCaseLidSheet(storageCase))
{
  caseLidSheet.Position = 0;

  using FileStream lidSheetPdf = new FileStream("lidsheet.pdf", FileMode.Create);
  caseLidSheet.CopyTo(lidSheetPdf);
}

using (MemoryStream labelsSheet = reportGenerator.GenerateContainerLabelsSheet(storageCase))
{
  labelsSheet.Position = 0;
  
  using FileStream labelsPdf = new FileStream("labelsheet.pdf", FileMode.Create);
  labelsSheet.CopyTo(labelsPdf);
}