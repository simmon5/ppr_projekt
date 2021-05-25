from xmlrpc.server import SimpleXMLRPCServer
from xmlrpc.server import SimpleXMLRPCRequestHandler
import xmlrpc.client
import base64

print("Proces 4")

# Restrict to a particular path.
class RequestHandler(SimpleXMLRPCRequestHandler):
    rpc_paths = ('/RPC2',)

# Create server
server = SimpleXMLRPCServer(('localhost', 10003),requestHandler=RequestHandler)
server.register_introspection_functions()

class MyFuncs:
    def abc(self, dane):
        print("Otrzymano wynik:",dane)

        return data

server.register_instance(MyFuncs())

def adder_function():
    print("test\n")
    return ("test")
server.register_function(adder_function, 'add')



# Run the server's main loop
server.serve_forever()
