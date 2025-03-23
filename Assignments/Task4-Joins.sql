--23 Retrive payment with courier information
Select p.PaymentID,p.Amount,c.CourierID from Payment as p
join Courier as c
on c.CourierID=p.CourierID

--24 Retrive payment with Location Information
Select p.Amount as Payment,l.LocationID,l.LocationName from Payment as p
join Location as l
on p.PaymentID=l.LocationID

--25 Retrive payments with courier and locaation information
 Select p.Amount,c.CourierID,c.SenderName,c.Status,l.LocationName from Payment as p
 join Courier as c
 on c.CourierID=p.CourierID
 join Location as l
 on p.LocationID=l.LocationID

 --26. List all payments with courier details
 Select * from Payment
 join Courier
 on Payment.CourierID=Courier.CourierID

 --27.Total payments received for each courier 
	select c.courierid, c.sendername, c.receivername, sum(p.amount) as totalpayments
	from courier c
	join payment p 
	on c.courierid = p.courierid
	group by c.courierid, c.sendername, c.receivername
	order by totalpayments desc;

--28. List payments made on a specific date
Select p.PaymentID,p.Amount,c.CourierId from Payment as p
join Courier as c
on c.CourierID=p.CourierID
where PaymentDate='2025-03-19';

--29.Get Courier Information for Each Payment 
Select p.PaymentID,p.Amount,c.CourierID,c.SenderName,c.ReceiverName,c.status from Payment as p
join Courier as c
on p.CourierID=c.CourierID

--30.Get Payment Details with Location
Select p.PaymentID,l.LocationID,p.Amount,l.LocationName,l.Address from Payment as p
join Location as l
on p.PaymentID=l.LocationID

--31.Calculating Total Payments for Each Courier 
select courierid,sum(amount) as total_payments
from payment
group by courierid
order by total_payments desc;

--32.List Payments Within a Date Range 
Select * from Payment
where PaymentDate between '2025-03-15' and '2025-03-20'

--33.Retrieve a list of all users and their corresponding courier records, including cases where there are no matches on either side 
select u.userid, u.name as username, c.courierid,c.sendername,c.senderaddress, c.receivername,c.receiveraddress,c.weight, 
c.status, c.trackingnumber,c.deliverydate
from usertable u
full outer join courier c
on u.name = c.sendername or u.name = c.receivername;

--34. Retrieve a list of all couriers and their corresponding services, including cases where there are no matches on either side
select  c.courierid,c.sendername,c.senderaddress, c.receivername,c.receiveraddress,c.weight, 
c.status, c.trackingnumber,c.deliverydate, cs.ServiceID,cs.ServiceName from Courier as c
full outer Join CourierServices as cs
on c.CourierID=cs.ServiceID

--35.Retrieve a list of all employees and their corresponding payments, including cases where there are no matches on either side 
select EmployeeID,Name,Salary from Employee

--36.List all users and all courier services, showing all possible combinations.
select u.UserID,c.ServiceID,u.Name,c.ServiceName from UserTable u
cross join CourierServices c

--37.List all employees and all locations, showing all possible combinations.
select e.EmployeeID,l.LocationID,e.Name,e.Role,l.LocationName from Employee e
cross join Location l

--38.Retrieve a list of couriers and their corresponding sender information (if available) 
select * from Courier
left join	UserTable
on Courier.SenderName=UserTable.Name

--39. Retrieve a list of couriers and their corresponding receiver information (if available):
select * from courier
left join usertable 
on courier.receivername = usertable.name;

--40.Retrieve a list of couriers along with the courier service details (if available)
select * from Courier
join CourierServices
on Courier.CourierID=CourierServices.ServiceID

--41.Retrieve a list of employees and the number of couriers assigned to each employee
 Select e.EmployeeID,e.Name,count(c.courierID) as ToatalCourier
 from Employee e
 join Courier c
 on e.EmployeeID=c.CourierID
 group by e.Name,e.EmployeeID

 --42.Retrieve a list of locations and the total payment amount received at each location
 select l.LocationId,l.LocationName,sum(p.Amount) as Total_Amount
 from Location l
 join Payment p
 on l.LocationID=p.LocationID
 group by l.LocationID,l.LocationName
 order by Total_Amount desc

 --43. Retrieve all couriers sent by the same sender (based on SenderName). 
SELECT SenderName, COUNT(CourierID) AS Total_Sent
FROM Courier
GROUP BY SenderName

--another method
select SenderName, STRING_AGG(CourierID, ', ') AS CouriersSent
from Courier
group by SenderName

--44.List all employees who share the same role. 
select e.employeeid, e.name, e.role
from employee e
where e.role in (
    select role from employee group by role having count(*) >= 1-- we can check here
)

--45.Retrieve all payments made for couriers sent from the same location
select c.senderaddress, p.paymentid, p.courierid, p.amount, p.paymentdate
from payment p
join courier c on p.courierid = c.courierid
where cast(c.senderaddress as varchar(max)) in (
    select cast(senderaddress as varchar(max)) from courier 
    group by cast(senderaddress as varchar(max)) 
    having count(*) > 1
)

--46. Retrieve all couriers sent from the same location (based on SenderAddress). 
select 
    cast(c.senderaddress as varchar(max)) as senderaddress, 
    l.locationid, 
    l.locationname, 
    count(c.courierid) as totalcouriers
from courier c
join location l
on cast(c.senderaddress as varchar(max)) = cast(l.address as varchar(max))
group by cast(c.senderaddress as varchar(max)), l.locationid, l.locationname;

--47. List employees and the number of couriers they have delivered:
-- as there is no relation between the employee and courier table.I am going to add the employeeId as foreign key in the courier Table and make a reference 
-- from the Employee Table

ALTER TABLE Courier
ADD EmployeeID INT;

ALTER TABLE Courier
ADD CONSTRAINT FK_Courier_Employee
FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID);

UPDATE Courier SET EmployeeID = 1 WHERE CourierID IN (1, 3, 5);
UPDATE Courier SET EmployeeID = 2 WHERE CourierID IN (2, 4, 6);
UPDATE Courier SET EmployeeID = 3 WHERE CourierID IN (7, 8, 9, 10);
UPDATE Courier SET EmployeeID = 4 WHERE CourierID IN (11, 13, 15);
UPDATE Courier SET EmployeeID = 5 WHERE CourierID IN (12, 14, 16);
UPDATE Courier SET EmployeeID = 6 WHERE CourierID IN (17, 18, 19, 20);

SELECT e.EmployeeID, e.Name, COUNT(c.CourierID) AS TotalCouriersDelivered
FROM Employee e
LEFT JOIN Courier c ON e.EmployeeID = c.EmployeeID
GROUP BY e.EmployeeID, e.Name
ORDER BY EmployeeID ;

--48. Find couriers that were paid an amount greater than the cost of their respective courier services
-- as like previous there is no relation between the courier services table and courier table so I put the serviceID as primary key in the courier table
-- and made a reference from the courier Services

ALTER TABLE Courier
ADD ServiceID INT

ALTER TABLE Courier
ADD CONSTRAINT FK_CourierServices
FOREIGN KEY (ServiceID) REFERENCES CourierServices(ServiceID);
UPDATE Courier SET ServiceID = 1 WHERE CourierID = 1; 
UPDATE Courier SET ServiceID = 2 WHERE CourierID = 2; 
UPDATE Courier SET ServiceID = 3 WHERE CourierID = 3; 
UPDATE Courier SET ServiceID = 4 WHERE CourierID = 4; 
UPDATE Courier SET ServiceID = 5 WHERE CourierID = 5; 
UPDATE Courier SET ServiceID = 6 WHERE CourierID = 6; 
UPDATE Courier SET ServiceID = 7 WHERE CourierID = 7; 
UPDATE Courier SET ServiceID = 8 WHERE CourierID = 8; 
UPDATE Courier SET ServiceID = 9 WHERE CourierID = 9; 
UPDATE Courier SET ServiceID = 10 WHERE CourierID = 10; 




select p.CourierID, c.SenderName, c.ReceiverName, cs.ServiceName, cs.Cost AS ServiceCost, p.Amount AS PaidAmount
from Payment p
join Courier c on p.CourierID = c.CourierID
join CourierServices cs on c.ServiceID = cs.ServiceID
where p.Amount > cs.Cost;
--upto joins completed	



























