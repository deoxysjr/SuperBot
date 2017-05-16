//maanden
public static string Month = "januari";

if(DateTime.Month == 1)
{
	Month = "januari";
}
if(DateTime.Month == 2)
{
	Month = "feberuari";
}
if(DateTime.Month == 3)
{
	Month = "maart";
}
if(DateTime.Month == 4)
{
	Month = "april";
}
if(DateTime.Month == 5)
{
	Month = "mei";
}
if(DateTime.Month == 6)
{
	Month = "juni";
}
if(DateTime.Month == 7)
{
	Month = "juli";
}
if(DateTime.Month == 8)
{
	Month = "augustus";
}
if(DateTime.Month == 9)
{
	Month = "septemder";
}
if(DateTime.Month == 10)
{
	Month = "oktober";
}
if(DateTime.Month == 11)
{
	Month = "november";
}if(DateTime.Month == 12)
{
	Month = "december";
}
//specialen dagen
public static string SPday = "new year";
public bool SpDay = false;

if(SpDay == false)
{
	return;
}	
else
{
    if(DateTime.DayOfYear == 1)
    {
        SPday = "new year";
    }
    if(DateTime.DayOfYear == 366)
    {
        SPday = "schrikel jaar";
    }
}

