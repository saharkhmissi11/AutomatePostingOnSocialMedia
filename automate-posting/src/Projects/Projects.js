import React, { useState, useEffect } from 'react';
import './Projects.css'
import client, { config } from '../utils';
import Medias from '../Medias/Medias';
import Alert from 'react-bootstrap/Alert';

const Projects=({started})=> {
  const [projects, setProjects] = useState([]);
  const [productsInProject, setProductsInProject] = useState([]);
  const [selectedProject, setSelectedProject] = useState();
  const folderPath = '/projects'
  useEffect(() => {
   
    // Fetch image links from your API endpoint    
    client.get(`DropBox/GetFolders/${encodeURIComponent(folderPath)}`, { headers: { 'Content-Type': 'application/json', } })
      .then(response => {
        setProjects(response.data);
      })
      .catch(error => {
        console.error('Error fetching image links:', error);
      });
  }, []);
  const onProjectSelect = (project) => {
    setSelectedProject(project)
  }
  console.log("started",started)
  return (
    <>
     {started && <Alert>This is an alert</Alert>}
    <br></br>
      <div className="projectsContainer">
        <h2>Projects</h2><br></br>
        <div class="folder-list">
          {
              projects.map((name, index) => (
            <>
              <div class="folder">
                <span class="folder-icon">&#128193;</span>
                <span onClick={() => onProjectSelect(name)}  class="folder-name">{name}</span>
              </div>
              {selectedProject &&selectedProject==name && (<Medias project={selectedProject}></Medias>)}
            </>
              ))
          }
  </div>
      </div>
      
    </>

  );
}
export default Projects;
