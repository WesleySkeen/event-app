import React from 'react';
import Events from '../events/events'
import SearchBar from '../search-bar/search-bar';
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

export default class App extends React.Component {
  
  public render() {
    
    return (
      <div className="App">
        <Container>
          <Row>
          <Col md="4">          
            <SearchBar />
          </Col>
          <Col md="8">          
          <Events />
          </Col>
          </Row>          
        </Container>                    
      </div>
    );
  } 
}