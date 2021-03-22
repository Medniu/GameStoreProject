CREATE FUNCTION [dbo].[GetValue](@ProductID int)  
RETURNS decimal   
AS   
-- Returns the stock level for the product.  
BEGIN  
    DECLARE @ret decimal;  
    SELECT @ret = AVG(Rating)   
    FROM productRatings    
    WHERE ProductID = @ProductID                 
    RETURN @ret;  
END; 
