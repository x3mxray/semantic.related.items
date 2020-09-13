# Sitecore Cortex: Related items Module 
![Related items](documentation/images/1.jpg?raw=true "Related items")

This module allows you automatically find related items that have simiral semantic content.

Solution is based on NLP, wodr2vec algorithm.

If you want to know how it works more detailed, you can see [this video](https://www.youtube.com/watch?v=XC2tgfUwuWA&ab_channel=SUGBelarus).

## Pre-requisites
Sitecore 9.1.0  and higher.

## Installation

- Install the sitecore package [RelatedItems-1.0.zip](https://github.com/x3mxray/semantic.related.items/blob/master/install/RelatedItems-1.0.zip)

- Copy [GoogleNews.bin](https://github.com/x3mxray/semantic.related.items/blob/master/install/GoogleNews.bin) dataset to App_Data folder. If you want to use other vectorized datasets, you can find them in internet (for example [here](https://fasttext.cc/docs/en/english-vectors.html)).

- Create new core "sitecore_related_content_index" in Solr (or copy existing empty from here: [sitecore_related_content_index](https://github.com/x3mxray/semantic.related.items/blob/master/install/sitecore_related_content_index.zip)).
If you create new one, add new dynamic field for floats in **managed-schema** of your solr core:
```
<dynamicField name="*_fs" type="pfloat" multiValued="true" indexed="true" stored="true"/>
```

- If you need my demo content fot tesing, install package [test_content.zip](https://github.com/x3mxray/semantic.related.items/blob/master/install/test_content.zip).


## Settings and configuration
- App_Config\Include\Foundation\Foundation.RelatedContentTagging.Indexing.config:
  - Change index configuration for templates that you want to use in reladet items:
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

# Feedback #
If you are faced with any issues or have questions/suggestions you can contact me in sitecore slack channel @x3mxray.
