This project implements the totally ordered Multicast in distributed system. There are five programs simulating a mulicast group. 
The sender of a multicast will attach a proposed timestamp on the multicast message.
The middleware receives message(but not yet deliver) and sends back the priority(Lamport timestamp) it allocated to the message in the current queue and mark the message as 'undeliverable'
The sender of the multicast chooses the largest timestamp as the agreed timestamp and multicast to the group.
Upon receiving the agreed timestamp, middleware on receivers mark the corresponding message as 'deliverable' and sort the list of received message
Only the message with smallest timestamp and marked as 'deliverable' can be delivered to the actually program.



To identify a multicast message:

There are three types of messages to be multicast: The origin multicast message ‘Msg', the
‘ACK' message with the proposed timestamp and the ‘FinAck' message with the final agreed
timestamp.

Msg:
The origin multicast message below indicates that it is the first message multicast by
Middleware 1 with the timestamp (1,M1):

          Message received: Msg#1 from Middleware 1: (1, M1) <EOM>


ACK:
The ACK message is the message that sent back from the receiver with the proposed timestamp.
The message below indicated that this is an ACK message for ‘Msg #1' sent by Middleware 5,
with the proposed timestamp (2,M1):

          Message received: ACK for Msg #1 from Middleware 5: (2, M1) <EOM>

FinAck:
The FinAck message is multicast by the sender to notify the final agreed timestamp for the
origin multicast message. The FinAck message below identifies that this is a final acknowledge
message for ‘Msg #1' from Middleware 1, with the final agreed timestamp (6,M1):

          Message received: FinAck for Msg #1 from Middleware 1: (6, M1) <EOM>
          
