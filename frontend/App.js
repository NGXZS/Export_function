import ExportCSV from './ExportCSV';

const App = () => {
  return (
    <ExportCSV />
  );
}

/* import React, { Component } from 'react';

class App extends Component {
  constructor(props) { // called first, set up initial state
    super(props); // inherit parents props
    this.state = {
      items: [],
      isLoaded: false,
    }
  }

  componentDidMount() { //after components are render()
    fetch("https://localhost:44365/api/Book/GetAllBooks") //https://jsonplaceholder.typicode.com/posts
      .then(res => res.json())
      .then(data => {
        console.log(data); //verification

        this.setState({
          isLoaded: true,
          items: data,
        })
      });
  }

  render() { //produces output
    var { isLoaded, items } = this.state;
    if (!isLoaded) {
      return <div>not loaded</div>
    } else {
      return (
        <div className="App">
          Loaded awesome!
          <ul>
            {items.map(item => (
              <li key={item.Id}>
                Id= {item.Id} |
                StartDis={item.StartDis} |
                EndDis = {item.EndDis} |
                string1 = {item.string1} |
                string2 = {item.string2}
              </li>
              //how to export this data out?
              //how to export a range?
            ))}
          </ul>
        </div>
      )
    }
  }
}
*/

export default App;
