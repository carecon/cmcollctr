[wmi]"ROOT\ccm\invagt:InventoryActionStatus.InventoryActionID='{00000000-0000-0000-0000-000000000003}'" | remove-wmiobject; [void]([wmiclass]'ROOT\ccm:SMS_Client').TriggerSchedule('{00000000-0000-0000-0000-000000000003}'); "Heartbeat"