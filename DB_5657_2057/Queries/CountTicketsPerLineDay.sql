/* 
	0 param - date
	Date format: 'yyyy-mm-dd'
*/

SELECT line_id, COUNT(*) AS count
FROM tlines
NATURAL JOIN tickets
NATURAL JOIN payments
WHERE payment_date = {0}
GROUP BY line_id