# MongoDB

https://www.youtube.com/watch?v=fKyaj-iWUmg

Mongo DB tools - mongo.exe(command line shell),mongodb compass(comes along with mongo db), Robomongo, LINQPad

Install mongodb
https://docs.mongodb.com/tutorials/install-mongodb-on-windows/

create a configuration, also create the directory for data and log.

create a windows service to automatically start the service.

C:\WINDOWS\system32>"C:\Program Files\MongoDB\Server\3.6\bin\mongod.exe" --confi
g "C:\Program Files\MongoDB\Server\3.6\bin\mongod.cnf" --install

make sure the service is running. configure it to start automatically. This will now make sure that the service is always available.

Install RoboMongo to manipulate db. This is a UI management studio.

When you map a class to collection, If you have a unique id then you can use that and specify an attribute [BsonId]. If no id is specified then it will throw an error while reading. It will automatically insert an id field into database.

nuget install MongoDB.Driver

Once you run the c# code, then the database will be created if not already available.

in config set the below to enable auth.

security:
    authorization: enabled

MongoDB does not support user defined functions (UDFs) out-of-the-box. But it allows creating and saving JavaScript functions using the db.system.js.save command. The JavaScript functions thus created can then be reused in the MapReduce functions.

actual config settings for mongodb.

security:
    authorization: enabled

systemLog:
    destination: file
    path: C:\MongoDb\log\mongod.log
storage:
    dbPath: C:\MongoDb\db

After this we have to restart the mongodb server. As we have a windows service running for this, we only have to restart that service.

*****************mongo db conf*******************************
# mongod.conf

# for documentation of all options, see:
#   http://docs.mongodb.org/manual/reference/configuration-options/

# Where and how to store data.
storage:
  dbPath: /var/lib/mongodb
  journal:
    enabled: true
#  engine:
#  mmapv1:
#  wiredTiger:

# where to write logging data.
systemLog:
  destination: file
  logAppend: true
  path: /var/log/mongodb/mongod.log

# network interfaces
net:
  port: 27017
  bindIp: 0.0.0.0

#processManagement:

#security:
#auth = true
#operationProfiling:

#replication:

#sharding:

## Enterprise-Only Options:

#auditLog:
#snmp:

*****************mongo db conf end here***********************

If using robomongo, then setting users via the UI may not work. You have to use the query to create an user and the role. You have to go to admin database and set the user account.

db.createUser(
   {
       user: "tom", 
       pwd: "jerry", 
       roles:["userAdminAnyDatabase","readWriteAnyDatabase","dbAdminAnyDatabase","clusterAdmin"]
   })

Use connection string instead of connection setings while connecting to the database via the driver.


*********************Replica database and failover***********
We need multiple instances of the database server.
locally create multiple instances of the config file. Modify port number and enable replica.

