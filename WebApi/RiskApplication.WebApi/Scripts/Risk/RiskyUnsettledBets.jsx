var UnsettledBets = React.createClass({

    displayName: 'UnsettledBets',
    loadRiskyUnsettledBets: function() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },

    getInitialState: function() {
        return {data: []};
    },

    componentDidMount: function() {
        this.loadRiskyUnsettledBets();
    },

    render: function() {
        return (

         <div className = "container" > 
            <div class="row">
                <div className="span4">< h1 > Customer’s Unsettled Bets< /h1></div><br />
                <div className="span4"> <img src="/Content/Images/b94a48.png" height="10" width="10"/> <b>Risky Bets</b> (Unusual Rate Winners)</div><br />
                <div className="span4"> <img src="/Content/Images/428bca.png" height="10" width="10"/> <b>Unusual Bets</b> (Stake is more than 10 times higher than that customer’s average bet)</div><br />
                <div className="span4"> <img src="/Content/Images/468847.png" height="10" width="10"/> <b>Highly Unusual Bets</b> (Stake is more than 30 times higher than that customer’s average bet)</div><br />
                <div className="span4"><BetsList data={this.state.data}/></div></div></div>
   
        );
    }
});

var BetsList = React.createClass({
    render: function() {
        var bets = this.props.data.map(function(bet, index){
            return (
                <UnsettledBet data= {bet} key={index}/>
            );
});
return (
    <table className="table table-bordered">
    <thead>
        <tr>
            <th>Customer Id</th>
            <th>Event Id</th>
            <th>Participant</th>
            <th>Stake</th>
            <th>To Win</th>
            <th>Unusual</th>
            <th>Highlu Unusual</th>
        </tr>
    </thead>
    <tbody>
            {bets}
    </tbody>
    </table>
        );
}
});

var UnsettledBet = React.createClass({
    render: function() {
            var bet = this.props.data;

            var riskyClass = bet.IsBetFromUnusualWinner ? "danger" : "";

            return (
            < tr
            className = { riskyClass } > 
                < td > { bet.CustomerId } < /td>
                < td > { bet.EventId } < /td>
                < td > { bet.Participant } < /td>
                < td > { bet.Stake } < /td>
                < td > { bet.ToWin.toLocaleString('en-US', {minimumFractionDigits: 2 })} < /td>
                < td >  < img src="/Content/Images/428bca.png" height="5" width="5" style={bet.IsUnusualBet ? {} : { display:'none' }}/></td>
                <td> <img src="/Content/Images/468847.png" height="5" width="5" style={bet.IsHighlyUnusualBet ? {} : { display:'none' }}/></td>
            </tr>
        );
}
});

ReactDOM.render(
  <UnsettledBets url="/apimvc/unsettledbets" />,
  document.getElementById('unsettledcontent')
);