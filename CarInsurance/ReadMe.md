# Insurance Application

One of my big projects. This project involved me creating the app from SQL to front-end.

![insuranceApp](insuranceAppDisplay.png)



```sql
CREATE TABLE [dbo].[Insurees] (
    [Id]              INT           IDENTITY (0, 1) NOT NULL,
    [FirstName]       NVARCHAR (50) NOT NULL,
    [LastName]        NVARCHAR (50) NOT NULL,
    [EmailAddress]    NVARCHAR (50) NOT NULL,
    [DateOfBirth]     DATETIME      NOT NULL,
    [CarYear]         INT           NOT NULL,
    [CarMake]         NVARCHAR (50) NOT NULL,
    [CarModel]        NVARCHAR (50) NOT NULL,
    [DUI]             BIT           NOT NULL,
    [SpeedingTickets] INT           NOT NULL,
    [CoverageType]    BIT           NOT NULL,
    [Quote]           MONEY         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```
Creating the model using ADO. Entity Data Model and creating a script that use the information to figure out the estimate price

```csharp
 [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            //Age
            var DOB = insuree.DateOfBirth.Year;
            int fix = DOB + DOB;
            int thisYear = DateTime.Now.Year;
            int age = fix - thisYear; 
            int Operation = 50 ; //Base of 50
            string me = "o";
            int finalSum;
            if (insuree.Quote >= 0)
            {
                if(age > 1) 
                {
                    //Age
                    if (age <= 18)
                    {
                        Operation += 100;
                    }
                    if (Enumerable.Range(19, 25).Contains(age))
                    {
                        Operation += 50;
                    }
                    if (age >= 25)
                    {
                        Operation += 25;
                    }
                }
                if(insuree.CarYear > 0)
                {
                    //Car Year
                    if (insuree.CarYear < 2000)
                    {
                        Operation += 25;
                    }
                    if (insuree.CarYear > 2015)
                    {
                        Operation += 25;
                    }
                }
                
                if(insuree.CarMake.Length > me.Length) 
                {
                    //Car Make
                    if (insuree.CarMake == "Porsche")
                    {
                        Operation += 25;
                    }
                    if (insuree.CarMake == "Porsche" && insuree.CarModel == "911 Carrera")
                    {
                        Operation += 25;
                    }
                }
                

                //Speeders
                if (insuree.SpeedingTickets > 0)
                {
                    Operation += insuree.SpeedingTickets * 10;
                }
                //DUI
                if (insuree.DUI == true)
                {
                    Operation += Operation / 4;
                }

                //Coverage
                if (insuree.CoverageType == true)
                {
                    Operation += Operation / 4;
                    finalSum = Operation * 12;
                    insuree.Quote = finalSum;
                }
                if (insuree.CoverageType == false)
                {
                    
                    finalSum = Operation * 12;
                    insuree.Quote = finalSum;
                    
                }
            }
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }
```
![](insuranceApp.gif)

And Using htmlHelper and Razor to render the HTML elements and display some calculations

```csharp
            <td class="estmate">
              @if (Model.CoverageType == true)
              {
                @finalOne
              }
              @if (Model.CoverageType == false)
              {
                <p>0</p>

              }
            </td>
```

And finally some CSS 

```css
.createContainter {
    width: 700px !important;
    margin: 5px auto;
    border: 1px solid black;
    padding: 40px 50px;
    -webkit-box-shadow: -5px -1px 17px 5px rgba(0,0,0,0.75);
    -moz-box-shadow: -5px -1px 17px 5px rgba(0,0,0,0.75);
    box-shadow: -5px -1px 17px 5px rgba(0,0,0,0.75);
    background-color: #343a40;
    color: white;
}
body {
    background-image: url('../../Content/buildings-1851246.jpg');
    background-repeat: no-repeat;
    background-size: cover;
```




