/*const onBeginProcess = async () => {
    try {
      const croppedImagesData = await Promise.all(
        mediaRequests.map(async (media, index) => {
          const mediaResponse = await client.get(`Database/getMediaIdByUrl/${encodeURIComponent("/shootingflow/projects/"+project+"/medias/"+product+"/"+media.name)}`);
          const mediaData = mediaResponse.data;
           console.log("iffffffffffff",mediaData)
          const visibleProductsResponse = await client.get(`Database/getVisibleProducts/${mediaData}/${projectId}`);
          const visibleProductsData = visibleProductsResponse.data;
  
          const imagePath = await getCroppedImg(media.path, croppedAreaPixels[index], selectedPlatform);
  
          return {
            primaryProduct: product,
            secondaryProducts: visibleProductsData,
            imagePath: imagePath,
            platform: selectedPlatform
          };
        })
      );
  
      for (const croppedImageData of croppedImagesData) {
        await client.post("Process/begin", croppedImageData, {
          headers: {
            'Content-Type': 'application/json',
          },
        });
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };*/