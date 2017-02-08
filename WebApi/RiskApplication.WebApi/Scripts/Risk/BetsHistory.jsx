var BetsHistory = React.createClass({
        displayName: 'BetHistory',
        loadBetsHistory: function() {
            var xhr = new XMLHttpRequest();
            xhr.open('get', this.props.url, true);
            xhr.onload = function() {
                var data = JSON.parse(xhr.responseText);
                this.setState({ data: data });
            }.bind(this);
            xhr.send();
        },

        getInitialState: function() {
            return { data: [] };
        },
        componentDidMount: function() {
            this.loadBetsHistory();
        },
  
        render: function() {
    return (
                    
    < div className = "container" > 
        <div class="row">
        <div className="span4">< h1 > Customer Betting History On Settled Bets< /h1></div><br />
        <div className="span4">  <img src="/Content/Images/b94a48.png" height="10" width="10"/> Unusual Rate Winners</div><br />
        <div className="span4"><CustomerList data = { this.state.data } /></div></div></div>
                
            );
    }
});
                

var CustomerList = React.createClass({
    render: function() {
        var customers = this.props.data.map(function(customer, index) {
                return (
                <Customer data={customer} key={index}/>
        );
    });
return (
    <table className="table table-bordered">
    <thead>
        <tr>
            <th>CustomerId</th>
            <th>Wins Count</th>
            <th>All Bets</th>
            <th>Average Bets</th>
        </tr>
    </thead>
    <tbody>
        {customers}
    </tbody>
    </table>
        );
}
});

var Customer = React.createClass({
        render: function() {
                var customer = this.props.data;
                var riskycatClass = customer.UnusualRateWinner ? "danger" : "";

                return (
                <tr className = { riskycatClass }> 
                
                    < td > { customer.CustomerId } < /td>
                    < td > { customer.WinsCount } < /td>
                    < td > { customer.BetsCount } < /td>
                    < td > { customer.AverageBet.toLocaleString('en-US', {minimumFractionDigits: 2}) } < /td>                        
                < /tr>
           
            
);
}
});

ReactDOM.render(
    <BetsHistory url="/apimvc/settledbets" /> ,
    document.getElementById('settledcontent')
);