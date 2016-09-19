using System.IO;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using Owin;

namespace demo.falcor
{
    public static class GQ
    {
        public static void UseGraphQL(this IAppBuilder appBuilder, string route)
        {
            appBuilder.Map(route, app => { app.Use<GraphQLMiddleware>(); });
        }
    }

    //GET: http://localhost:9000/graphql?query=query { billy { id name} }
    /*
     * POST: http://localhost:9000/graphql
     *
     * {
	 *     "query":" { billy { id } }"
     * }
     *
     */
    public class GraphQLMiddleware : OwinMiddleware
    {
        public GraphQLMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var schema = new Schema
            {
                Query = new OrderQuery()
            };

            var bodyStream = context.Request.Body;
            var sr = new StreamReader(bodyStream);
            var body = sr.ReadToEnd();

            string query = null;
            string queryStringMethod = null;
            dynamic jsonGraph = null;

            if (string.IsNullOrEmpty(body))
            {
                query = context.Request.Query["query"];
            }
            else
            {
                var jobj = JObject.Parse(body);
                query = jobj["query"].ToString();
            }
            var payload = await ExecuteQuery(schema, query);
            context.Response.Headers.Set("content-type", "application/json");
            await context.Response.WriteAsync(payload);
        }

        static async Task<string> ExecuteQuery(
            Schema schema,
            string query,
            object rootObject = null,
            string operationName = null,
            Inputs inputs = null)
        {
            var executer = new DocumentExecuter();
            var writer = new DocumentWriter(true);

            var result = await executer.ExecuteAsync(
                schema,
                rootObject,
                query,
                operationName,
                inputs).ConfigureAwait(false);
            return writer.Write(result);
        }
    }

    //standard POCO
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
    }

    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string PurchaseOrderNumber { get; set; }
    }

    public class OrderQuery : ObjectGraphType
    {
        public OrderQuery()
        {
            Name = "Query";
            Field<OrderType>("orders",
                resolve: context =>
                {
                    return new Order
                    {
                        Id = 1,
                        OrderNumber = "R2-D2"
                    };
                },
                description: "description"
            );
            Field<PurchaesOrderType>("purchaseOrders", resolve: context =>
            {
                return new PurchaseOrder
                {
                    Id = 3,
                    PurchaseOrderNumber = "C3P0"
                };
            });
        }
    }

    public class PurchaesOrderType : ObjectGraphType<PurchaseOrder>
    {
        public PurchaesOrderType()
        {
            Name = "PurchaseOrder";
            Field<NonNullGraphType<IntGraphType>>("Id", "The id of the order.");
            Field<StringGraphType>("PurchaseOrderNumber", "The name of the order.");
            IsTypeOf = value => value is PurchaseOrder;
            base.
        }
    }

    public class OrderType : ObjectGraphType
    {
        public OrderType()
        {
            Name = "Order";
            Field<NonNullGraphType<IntGraphType>>("Id", "The id of the order.");
            Field<StringGraphType>("OrderNumber", "The name of the order.");
            IsTypeOf = value => value is Order;
        }
    }
}
