/*
	{0} - line_id
*/

SELECT DISTINCT payment_date
FROM tickets
NATURAL JOIN payments
WHERE line_id = {0}
ORDER BY payment_date DESC