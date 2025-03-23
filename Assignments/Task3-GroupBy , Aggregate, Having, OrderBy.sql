--14.Find the total number of courier handles by each employee
CREATE TABLE Employee_Courier (
    EmployeeID INT,
    CourierID INT,
    PRIMARY KEY (EmployeeID, CourierID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (CourierID) REFERENCES Courier(CourierID)
);

select * from Employee_Courier
SELECT e.EmployeeID, e.Name, COUNT(ec.CourierID) AS TotalCouriersHandled
FROM Employee e
LEFT JOIN Employee_Courier ec ON e.EmployeeID = ec.EmployeeID
GROUP BY e.EmployeeID, e.Name


--15.Claculate the total revenue generated on each location
Select * from Payment
Select * from Location
Select * from CourierServices
 Select l.LocationName ,Sum(p.Amount) as Total_Revenue from Location l
 left join Payment as p
 on l.LocationID=p.LocationID
 group by l.LocationName

 --16.Total number of couriers deleivered to each location
 --here we join 3 tables 
select l.locationname, count(c.courierid) as totaldeliveredcouriers
from location l
join payment p on l.locationid = p.locationid
join courier c on p.courierid = c.courierid
where c.status = 'delivered'
group by l.locationname;

--17.Find the courier with highest highest average delivering time
select top 1 c.courierid, avg(datediff(day, p.paymentdate, c.deliverydate)) as avg_delivery_time
from courier c
join payment p on c.courierid = p.courierid
where c.deliverydate is not null
group by c.courierid
order by avg_delivery_time desc;

--18. Find locations with total payments less than a certain amount

select l.locationid, l.locationname, sum(p.amount) as total_payments
from location l
left join payment p on l.locationid = p.locationid
group by l.locationid, l.locationname
having sum(p.amount) < 500; -- Replace 500 with the desired amount

--19. Calculate total payments per location

select l.locationid, l.locationname, sum(p.amount) as total_payments
from location l
left join payment p on l.locationid = p.locationid
group by l.locationid, l.locationname;

--20. Retrieve couriers who have received payments totaling more than $1000 in a specific location (LocationID = X)
select c.courierid, sum(p.amount) as total_payments
from courier c
join payment p on c.courierid = p.courierid
where p.locationid =  9
group by c.courierid
having sum(p.amount) > 1000;


--21.Retrieve couriers who have received payments totaling more than $1000 after a certain date

select c.courierid, sum(p.amount) as total_payments
from courier c
join payment p on c.courierid = p.courierid
where p.paymentdate > '2025-03-18'  
group by c.courierid
having sum(p.amount) > 1000;

--22. Retrieve locations where the total amount received is more than $5000 before a certain date
select l.locationid, l.locationname, sum(p.amount) as total_payments
from location l
join payment p on l.locationid = p.locationid
where p.paymentdate < '2025-03-18'
group by l.locationid, l.locationname
having sum(p.amount) > 5000;

