# Sitecore Cortex: Related items Module 
![Related items](documentation/images/1.jpg?raw=true "Related items")

This module allows you automatically find related items that have similar semantic content.
Module has similar to **Content Tagging** behavior and configuration, but adapted for finding related items and does not use third party services.

Solution is based on NLP, word2vec algorithm.

If you want to know how it works more detailed, you can see [this video](https://www.youtube.com/watch?v=XC2tgfUwuWA&ab_channel=SUGBelarus) and/or [this article](https://www.brimit.com/blog/cortex-related-semantic-items).

## Pre-requisites
Sitecore 9.1.0  and higher.

## Installation

- Install the sitecore package [RelatedItems-1.0.zip](https://github.com/x3mxray/semantic.related.items/blob/master/install/RelatedItems-1.0.zip)

- Copy [GoogleNews.bin](https://github.com/x3mxray/semantic.related.items/blob/master/install/GoogleNews.bin) dataset to App_Data folder. If you want to use other vectorized datasets, you can find them in internet (for example [here](https://fasttext.cc/docs/en/english-vectors.html)).

- Create new core **"sitecore_related_content_index"** in Solr (or copy existing empty from here: [sitecore_related_content_index](https://github.com/x3mxray/semantic.related.items/blob/master/install/sitecore_related_content_index.zip)).
If you create new one, add new dynamic field for floats in **managed-schema** of your solr core:
```
<dynamicField name="*_fs" type="pfloat" multiValued="true" indexed="true" stored="true"/>
```

- If you need my demo content for tesing, install package [test_content.zip](https://github.com/x3mxray/semantic.related.items/blob/master/install/test_content.zip).

## Usage

- A new template **"Related Content Tagging"**  appear after package installation  *(/sitecore/templates/Foundation/Related Content Tagging)*.

- Inherit templates that you want to use for related items tagging from **"Related Content Tagging"** template:
![Base templates](documentation/images/2.jpg?raw=true "Base templates")

- For each template (that you want to use for retaled items tagging) assign corresponding related templates (templates where you want to find related items) in **"Related templates"** field of **Tagging** section:
 ![Related templates](documentation/images/3.jpg?raw=true "Related templates")

- Rebuild **sitecore_related_content_index** *(it is needed only for first time installation to index alredy existing content)*.

- Select item in sitecore tree and click **"Related for item"** or **"Related for item and subitems"** option in ribbon menu to find related items.
Selected item (and optionally subitems) will populated with related items in **"Related items"** field.
Found related items in **"Related items"** field are automatically ordered by semantic similarity.

- OPTIONAL: If you need to have only "really" similar related items in **"Related items"** field, you can specify **"Minimum similarity"** field in range from 0 to 100 (percent of similarity). 
**"Minimum similarity"** field is optional. Recommended value for minimum similarity is ~80. 
For better understanding how percentage of text similarity is working you can test [by this online tool](https://dandelion.eu/semantic-text/text-similarity-demo/?text1=Cameron+wins+the+Oscar&text2=All+nominees+for+the+Academy+Awards&lang=auto&exec=true)
 ![Minimum similarity](documentation/images/4.jpg?raw=true "Minimum similarity")


## Settings and configuration
- App_Config\Include\Foundation\Foundation.RelatedContentTagging.Indexing.config:
  - Change index configuration for templates that you want to use in related items:
  ```
  <include hint="list:AddIncludedTemplate">
                            <Article>{DFA6299F-86E5-45E3-A2FD-91DB981B74AA}</Article>
                            <News>{88B0538E-3BE7-4F62-A810-84F94ACE36B3}</News>
                            <Product>{B8C3024A-379D-4DB6-A837-127F175C98E0}</Product>
  </include>
  ```
- App_Config\Include\Foundation\Foundation.RelatedContentTagging.config:
  - Path to vectorized dataset model:
  ```
  <setting name="SemanticDatasetFilePath" value="App_Data\GoogleNews.bin" />
  ```
  - Search index name (default is **sitecore_related_content_index**):
  ```
  <setting name="RelatedContentIndexName" value="sitecore_related_content_index"/>
  ```

- To add/remove/overwrite any steps in related content tagging logic add/remove/update processors in **RealtedContentTagging** pipeline group in **Foundation.RelatedContentTagging.config**
For example, if you do not want to take into account texts of datasources items (leave only text analysis of item content itself), remove or comment:
```
<processor type="Semantic.Foundation.RelatedContentTagging.Pipelines.TagContent.RetrieveContentFromDatasourceItems, Semantic.Foundation.RelatedContentTagging" resolve="true" />
``` 

## How and when information is updated
**sitecore_related_content_index** is configured with **syncMaster**  strategy. It means that when you change content of item  and save item then **getContent** processors from **RealtedContentTagging** are executed, recalculated information and updated it in **sitecore_related_content_index**

## Additional notes
If you use items serializers like Unicorn/TDS etc., make sure that index rebuild after sync is configured in serializer config files. Or, alternatively, rebuild **sitecore_related_content_index** manually after synchronization.


# Feedback #
If you are faced with any issues or have questions/suggestions you can contact me in sitecore slack channel @x3mxray.
