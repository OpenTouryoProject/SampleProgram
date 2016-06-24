IF EXISTS 
(SELECT 
 1 
FROM 
  Customers 
WHERE 
CustomerId = @p1)

UPDATE
  Customers
SET
  Name = @p2, RegionId = @p3
WHERE
  CustomerId = @p1
ELSE
INSERT INTO 
  Customers (CustomerId, Name, RegionId)
VALUES 
  (@p1, @p2, @p3)

INSERT INTO 
  Orders (CustomerId,ProductId,OrderDate)
VALUES 
  (@p4, @p5, @p6)


                      