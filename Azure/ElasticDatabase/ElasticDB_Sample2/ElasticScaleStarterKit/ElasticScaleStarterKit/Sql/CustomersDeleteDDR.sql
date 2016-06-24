DELETE FROM
  Orders
WHERE
  customerID = @P1

DELETE FROM
  Customers
WHERE
  customerID = @P1