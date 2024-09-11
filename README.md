# MetaExchange API

## Overview

The MetaExchange API is a Kestrel-based web service that provides an endpoint for executing market orders on multiple cryptocurrency exchanges. The API exposes the functionality through a Swagger interface, allowing users to submit orders and receive the best possible execution plan based on existing exchange data.

## How to Use

### Step 1: Launch the API

When you run the API project, it automatically starts a Kestrel server. A browser window will open, displaying the Swagger interface for interacting with the API. The Swagger UI makes it easy to test and interact with the available endpoints.

### Step 2: Access the API Endpoint

1. **Navigate to the Swagger Interface**:  
   Once the server is running, the Swagger UI will open in your browser at the following URL:
      https://localhost:7213/swagger/index.html
   
2. **Expand the 'POST' Request**:  
In the Swagger UI, you will see a list of available endpoints. Look for the **'POST'** method under the `Execution` controller, which is highlighted in light green. Click the drop down arrow next to the **'POST'** request to expand the section.

### Step 3: Submit an Order

1. **Click 'Try it out'**:  
After expanding the 'POST' section, click the **'Try it out'** button. This will open a text box where you can input the order details.

2. **Input the Order Data**:  
In the text box, you will see the following JSON structure:
```json
{
  "orderType": "string",
  "amount": 0
}
```

Replace the "orderType": "string" field with either 'Buy' or 'Sell' to indicate the direction of the market order.
Replace the "amount": 0 field with the desired number of Bitcoin (BTC) you wish to buy or sell.

for example like so:
```json
{
  "orderType": "Buy",
  "amount": 5
}
```

3. **Execute the Request**:  
After entering the order details, click the 'Execute' button located below the text box. This will send the request to the API and return a response with the best execution plan.

### Step 4: View the Response
Once the order is executed, you will see the result in the Response section below the 'Execute' button.
