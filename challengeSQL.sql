select 
	concat(c.LastName, ', ', c.FirstName) 'full name'
	,c.Age
	,o.OrderID
	,o.DateCreated
	,o.MethodofPurchase 'Purchase Method'
	,od.ProductNumber
	,od.ProductOrigin
from Customer c
inner join Orders o on c.PersonID = o.PersonID
inner join OrdersDetails od on od.OrderID = o.OrderID
where od.ProductID = 1112222333
