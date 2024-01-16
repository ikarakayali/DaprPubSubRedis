
Bu repo dapr ile redis streame subscribe olunduğunda
UseCloudEvents middleware'in kapatılmasına rağmen
stream içerisindeki veride "data" keyi olmadığında streame subscribe olunabildiği 
ancak veriye ulaşılamadığını göstermek amacıyla oluşturulmuştur.

1-) "sub" projesi çalıştırılarak subscriber aktif edilir.
(VS Code ile debug edilebilir)

2-) "pub" projes ile dapr tarafından üretilen veri streame yazılabilir.
Bu durumda dapr verinin önüne "data" keyini eklemektedir.

3-) "pub" yerien direk redis cli ile ilgili streame data yazılıp subscriber'ın mesajı nasıl handle ettiği gözlenebilir.
Bunun için; 


docker exec -it bbt-redis redis-cli

XADD "orders" *  "data" "{\"orderId\":3}"  #Dapr subscriber bu mesajı base64 olarak dönmektedir.

XADD "orders" *  "data1" "{\"orderId\":3}"  # Ancak data dışında hehangi bir key olduğunda boş string dönmektedir.

##Zebee Redis Exporter aşağıdaki formatta mesaj yazmaktadır.

XADD "zeebe:PROCESS_EVENT" *  "1704131938823" "{\"partitionId\":1,\"value\":{\"bpmnElementType\":\"EVENT_BASED_GATEWAY\",\"flowScopeKey\":2251799813685484,\"bpmnEventType\":\"UNSPECIFIED\",\"parentProcessInstanceKey\":-1,\"parentElementInstanceKey\":-1,\"tenantId\":\"<default>\",\"bpmnProcessId\":\"test-account-create-flow\",\"processDefinitionKey\":2251799813685382,\"processInstanceKey\":2251799813685484,\"elementId\":\"Gateway_1xqdt3f\",\"version\":1},\"key\":2251799813685516,\"timestamp\":1703665766283,\"position\":533,\"valueType\":\"PROCESS_INSTANCE\",\"brokerVersion\":\"8.3.4\",\"recordType\":\"EVENT\",\"sourceRecordPosition\":517,\"intent\":\"ELEMENT_ACTIVATED\",\"rejectionType\":\"NULL_VAL\",\"rejectionReason\":\"\",\"authorizations\":{\"authorized_tenants\":[\"<default>\"]},\"recordVersion\":1}"

