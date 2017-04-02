﻿using HtmlAgilityPack;
using Recipes.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recipes.Services.Parsers
{
	public class ChefStepsParser : PageParserBase
	{

		public ChefStepsParser()
		{

		}

		override protected void GetIngredients()
		{

            new object();
            //Debug.WriteLine(this.HtmlDocument.DocumentNode.InnerHtml);

            var div = GetIngredientsDiv();
			if (null != div)
			{
				var ingredientGroups = this.GetIngredientGroups(div);
				if (null != ingredientGroups)
				{
					foreach (var ingredientGroup in ingredientGroups)
					{
						var ingredients = this.GetIngredients(ingredientGroup);
						ingredients.ForEach(x => this.AddIngredient(x));
					}
				}
			}
		}


		#region Html
#if false
<!DOCTYPE html>
<html ng-app='freshSteps' ng-controller='AppController as app' xmlns:fb='http://ogp.me/ns/fb#'>
  <head ng-controller='HeadController as head'>
    <title ng-bind-html='head.meta.getTitle()'></title>
    <meta name='description' ng-attr-content='{{head.meta.dict.description}}' ng-if='head.meta.dict.description'>
    <meta name='keywords' ng-attr-content='{{head.meta.dict.keywords}}' ng-if='head.meta.dict.keywords'>
    <link ng-href='https://www.chefsteps.com{{head.meta.dict.canonical}}' ng-if='head.meta.dict.canonical' rel='canonical'>
    <meta ng-attr-content='https://www.chefsteps.com{{head.meta.dict.og.url}}' ng-if='head.meta.dict.og.url' property='og:url'>
    <meta content='404' name='prerender-status-code' ng-if='head.is404()'>
    <meta content='website' property='og:type'>
    <meta content='ChefSteps' ng-attr-content='{{head.meta.dict.og.title}}' property='og:title'>
    <meta content='ChefSteps is here to make cooking more fun. Get recipes, tips, and videos that show the whys behind the hows for sous vide, grilling, baking, and more.' ng-attr-content='{{head.meta.dict.og.description}}' property='og:description'>
    <meta content='https://d3awvtnmmsvyot.cloudfront.net/api/file/vCbc5JfR1KUQwWoPLsAL' ng-attr-content='{{head.meta.dict.og.image}}' property='og:image'>
    <meta content='summary_large_image' property='twitter:card'>
    <meta content='@chefsteps' property='twitter:site'>
    <meta content='ChefSteps' ng-attr-content='{{head.meta.dict.twitter.title}}' property='twitter:title'>
    <meta content='ChefSteps is here to make cooking more fun. Get recipes, tips, and videos that show the whys behind the hows for sous vide, grilling, baking, and more.' ng-attr-content='{{head.meta.dict.twitter.description}}' property='twitter:description'>
    <meta content='https://d3awvtnmmsvyot.cloudfront.net/api/file/vCbc5JfR1KUQwWoPLsAL' ng-attr-content='{{head.meta.dict.twitter.image}}' property='twitter:image'>
    <meta content='NOINDEX' name='ROBOTS' ng-if='head.meta.dict.noindex'>
    <meta charset='utf-8'>
    <meta content='initial-scale=1, maximum-scale=1, user-scalable=no, width=device-width' name='viewport'>
    <meta content='ChefSteps' name='apple-mobile-web-app-title'>
    <meta content='en' http-equiv='Content-Language'>
    <meta content='single_host_origin' name='google-signin-cookiepolicy'>
    <meta content='d64492c5d22d7921-60170f9c9b7267c6-g7179c5f7a7e64573-d' name='google-translate-customization'>
    <meta content='zaV8aQl8p4mWRpDlE0g7fskT_IQ43Z42rsHq0QchNNk' name='google-site-verification'>
    <meta content='app-id=816731096, affiliate-data=at=10lIEQ&amp;ct=MWSmartBanneriPhone' name='apple-itunes-app' ng-if='!head.meta.dict.sousVideTargeted'>
    <meta content='app-id=970115018, affiliate-data=at=10lIEQ&amp;ct=MWSmartBanneriPhone' name='apple-itunes-app' ng-if='head.meta.dict.sousVideTargeted'>
    <meta content='ChefSteps' name='apple-mobile-web-app-title'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-57x57.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='57x57'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-60x60.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='60x60'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-72x72.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='72x72'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-76x76.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='76x76'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-114x114.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='114x114'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-120x120.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='120x120'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-144x144.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='144x144'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-152x152.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='152x152'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/apple-touch-icon-180x180.png?v=yyy4wNKzaA' rel='apple-touch-icon' sizes='180x180'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/favicon-32x32.png?v=yyy4wNKzaA' rel='icon' sizes='32x32' type='image/png'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/favicon-194x194.png?v=yyy4wNKzaA' rel='icon' sizes='194x194' type='image/png'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/favicon-96x96.png?v=yyy4wNKzaA' rel='icon' sizes='96x96' type='image/png'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/android-chrome-192x192.png?v=yyy4wNKzaA' rel='icon' sizes='192x192' type='image/png'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/favicon-16x16.png?v=yyy4wNKzaA' rel='icon' size='16x16' type='image/png'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/manifest.json?v=yyy4wNKzaA' rel='manifest'>
    <link href='https://d92f495ogyf88.cloudfront.net/favicons/favicon.ico?v=yyy4wNKzaA' rel='shortcut icon'>
    <meta content='#da532c' name='msapplication-TileColor'>
    <meta content='https://d92f495ogyf88.cloudfront.net/favicons/mstile-144x144.png?v=yyy4wNKzaA' name='msapplication-TileImage'>
    <meta content='#ffffff' name='theme-color'>
    <link href='https://plus.google.com/106370441183974123730?rel=author'>
    <link href='/feed' rel='alternate' title='ATOM' type='application/atom+xml'>
    <link href='/feed' rel='alternate' title='RSS' type='application/rss+xml'>
    <link href='https://d3ro0sksttkvbt.cloudfront.net/assets-16f7fa4/css/library.css' rel='stylesheet' type='text/css'>
    <link href='https://d3ro0sksttkvbt.cloudfront.net/assets-16f7fa4/css/chefsteps.css' rel='stylesheet' type='text/css'>
    <link href='/apple-touch-icon.png' rel='apple-touch-icon'>
    <link href='//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css' rel='stylesheet' type='text/css'>
    <script type='text/javascript'>window.csConfig = {"env":"production","deploySHA":"16f7fa4","chefstepsEndpoint":"https://www.chefsteps.com","bloomApiEndpoint":"https://cs-bloom-api-production.herokuapp.com","bloomEnv":"production","bloomCommunityEndpoint":"https://cs-bloom-community-production.herokuapp.com","segmentWriteKey":"CohfhzCATDidS52kLILe3ZZ3mVYYgzsP","fsAssetRoot":"https://d3ro0sksttkvbt.cloudfront.net/assets-16f7fa4","facebookAppId":"380147598730003","stripePublishableKey":"pk_live_b1CQV9HfmNE8djLHCKpykTxc","shopifyEndpoint":"https://store.chefsteps.com"};</script>
    <script crossorigin='anonymous' src='https://cdnjs.cloudflare.com/ajax/libs/lodash.js/3.10.1/lodash.min.js'></script>
    <script type='text/javascript'>window.save_underscore = window._;</script>
    <script crossorigin='anonymous' src='https://d3ro0sksttkvbt.cloudfront.net/assets-16f7fa4/js/library.js'></script>
    <script crossorigin='anonymous' src='https://d3ro0sksttkvbt.cloudfront.net/assets-16f7fa4/templates/templates.js'></script>
    <script crossorigin='anonymous' src='//cdn.jsdelivr.net/algoliasearch/3/algoliasearch.min.js'></script>
    <script crossorigin='anonymous' src='https://d3ro0sksttkvbt.cloudfront.net/assets-16f7fa4/js/chefsteps.js'></script>
    <script src='//api.filepicker.io/v2/filepicker.js'></script>
    <script async="" src='//f.vimeocdn.com/js/froogaloop2.min.js'></script>
    <script async="" src='https://www.youtube.com/iframe_api'></script>
    <script async="" onload="window.stripeHandler = StripeCheckout.configure({key: 'pk_live_b1CQV9HfmNE8djLHCKpykTxc', locale: 'auto'});" src='https://checkout.stripe.com/checkout.js'></script>
    <script async="" crossorigin='anonymous' src='https://photorankstatics-a.akamaihd.net/81b03e40475846d5883661ff57b34ece/static/frontend/latest/build.min.js'></script>
    <script>
      function onOlapicLoad(){
        OlapicSDK.conf.set('apikey', 'e242136515ecc58764343a1023408c3683dc6fad7e6195185bc16c039c7f6888');
      
        window.olapic = window.olapic || new OlapicSDK.Olapic( function(o){
          window.olapic = o;
        })
      }
    </script>
    <script>
      window.youtubeAPIReady = false;
      window.onYouTubeIframeAPIReady = function() {
        window.youtubeAPIReady = true;
      }
    </script>
    <script>
      window.twttr = (function (d,s,id) {
          var t, js, fjs = d.getElementsByTagName(s)[0];
          if (d.getElementById(id)) return; js=d.createElement(s); js.id=id;
          js.src="//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs);
          return window.twttr || (t = { _e: [], ready: function(f){ t._e.push(f) } });
        }(document, "script", "twitter-wjs"));
    </script>
    <script>
      !function(){var analytics=window.analytics=window.analytics||[];if(!analytics.initialize)if(analytics.invoked)window.console&&console.error&&console.error("Segment snippet included twice.");else{analytics.invoked=!0;analytics.methods=["trackSubmit","trackClick","trackLink","trackForm","pageview","identify","group","track","ready","alias","page","once","off","on"];analytics.factory=function(t){return function(){var e=Array.prototype.slice.call(arguments);e.unshift(t);analytics.push(e);return analytics}};for(var t=0;t<analytics.methods.length;t++){var e=analytics.methods[t];analytics[e]=analytics.factory(e)}analytics.load=function(t){var e=document.createElement("script");e.type="text/javascript";e.async=!0;e.src=("https:"===document.location.protocol?"https://":"http://")+"cdn.segment.com/analytics.js/v1/"+t+"/analytics.min.js";var n=document.getElementsByTagName("script")[0];n.parentNode.insertBefore(e,n)};analytics.SNIPPET_VERSION="3.0.1";}}();
    </script>
    <script>
      (function(w, d){
       var id='embedly-platform', n = 'script';
       if (!d.getElementById(id)){
         w.embedly = w.embedly || function() {(w.embedly.q = w.embedly.q || []).push(arguments);};
         var e = d.createElement(n); e.id = id; e.async=1;
         e.src = ('https:' === document.location.protocol ? 'https' : 'http') + '://cdn.embedly.com/widgets/platform.js';
         var s = d.getElementsByTagName(n)[0];
         s.parentNode.insertBefore(e, s);
       }
      })(window, document);
    </script>
  </head>
  <body ng-class="[app.mobileClass(), app.modalsClass, app.navClass, {'show-collection-nav': app.showCollectionNav}]">
    <div class='nav-spacer'></div>
    <div class='cs-icons-sprite-sheet'>
      <?xml version="1.0" encoding="utf-8"?><!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"><symbol id="arrow-large" viewbox="0 0 20 14" enable-background="new 0 0 20 14" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M20,7c0,0.6-0.5,1-1,1H3.4l4.3,4.3c0.4,0.4,0.4,1,0,1.4C7.5,13.9,7.3,14,7,14c-0.3,0-0.5-0.1-0.7-0.3l-6-6
      		C0.2,7.6,0.1,7.5,0.1,7.4c0-0.1,0-0.1,0-0.1C0,7.2,0,7.1,0,7c0-0.3,0.1-0.5,0.3-0.7l6-6c0.4-0.4,1-0.4,1.4,0c0.4,0.4,0.4,1,0,1.4
      		L3.4,6H19C19.6,6,20,6.5,20,7z"></path>
      </g>
      </symbol><symbol id="arrow-small" viewbox="0 0 13.5 9.5" enable-background="new 0 0 13.5 9.5" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M13.5,4.8c0,0.4-0.3,0.8-0.8,0.8H2.6l2.7,2.7c0.3,0.3,0.3,0.8,0,1.1C5.1,9.5,5,9.5,4.8,9.5
      		c-0.2,0-0.4-0.1-0.5-0.2l-4-4C0.1,5.2,0,5.1,0,4.9c0-0.1,0-0.1,0-0.2c0,0,0-0.1,0-0.1c0-0.2,0.1-0.3,0.2-0.4l4-4
      		c0.3-0.3,0.8-0.3,1.1,0c0.3,0.3,0.3,0.8,0,1.1L2.6,4h10.2C13.2,4,13.5,4.4,13.5,4.8z"></path>
      </g>
      </symbol><symbol id="bulb" viewbox="0 0 26.3 28.7" enable-background="new 0 0 26.3 28.7" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M15.7,21.7h-5c-0.6,0-1.1-0.4-1.2-1c0-0.2-0.1-0.4-0.1-0.4c-0.1-0.9-0.2-2-0.3-2.4C7.8,16.7,7,15.1,7,13.3
      		c0-3.3,2.7-6.1,6-6.1l0.1,0c3.3,0,6.1,2.7,6.1,6c0,1.8-0.7,3.5-2.1,4.7c-0.1,0.4-0.2,1.4-0.3,2.4c0,0,0,0.1,0,0.4
      		C16.8,21.3,16.3,21.7,15.7,21.7z M11.4,19.7H15c0.2-2.7,0.5-3,0.8-3.2c1-0.8,1.5-2,1.5-3.3c0-2.2-1.9-4-4.1-4
      		c-2.3,0-4.1,1.9-4.1,4.1c0,1.2,0.6,2.4,1.5,3.2C10.8,16.7,11.2,17,11.4,19.7z"></path>
      	<path fill="currentColor" d="M15.2,24.9h-4.1c-0.6,0-1-0.4-1-1s0.4-1,1-1h4.1c0.6,0,1,0.4,1,1S15.8,24.9,15.2,24.9z"></path>
      	<path fill="currentColor" d="M13.2,6.1c-0.6,0-1-0.4-1-1V1c0-0.6,0.4-1,1-1s1,0.4,1,1v4.1C14.2,5.6,13.7,6.1,13.2,6.1z"></path>
      	<path fill="currentColor" d="M7.5,8.5c-0.3,0-0.5-0.1-0.7-0.3L3.9,5.3c-0.4-0.4-0.4-1,0-1.4s1-0.4,1.4,0l2.9,2.9c0.4,0.4,0.4,1,0,1.4
      		C8,8.4,7.7,8.5,7.5,8.5z"></path>
      	<path fill="currentColor" d="M5.1,14.2H1c-0.6,0-1-0.4-1-1s0.4-1,1-1h4.1c0.6,0,1,0.4,1,1S5.6,14.2,5.1,14.2z"></path>
      	<path fill="currentColor" d="M18.9,8.5c-0.3,0-0.5-0.1-0.7-0.3c-0.4-0.4-0.4-1,0-1.4l2.9-2.9c0.4-0.4,1-0.4,1.4,0s0.4,1,0,1.4l-2.9,2.9
      		C19.4,8.4,19.1,8.5,18.9,8.5z"></path>
      	<path fill="currentColor" d="M25.3,14.2h-4.1c-0.6,0-1-0.4-1-1s0.4-1,1-1h4.1c0.6,0,1,0.4,1,1S25.9,14.2,25.3,14.2z"></path>
      	<circle fill="currentColor" cx="13.2" cy="27.4" r="1.3"></circle>
      </g>
      </symbol><symbol id="c-active" viewbox="0 0 12.6 12.9" enable-background="new 0 0 12.6 12.9" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M6.4,12.9c-1.8,0-3.4-0.7-4.6-2C0.6,9.7,0,8.2,0,6.3C0,4.6,0.6,3,1.9,1.8C3.1,0.6,4.7,0,6.4,0
      		c2.9,0,5.4,1.9,6.2,5H9.3C8.7,3.7,7.7,3.1,6.4,3.1c-1.9,0-3.3,1.4-3.3,3.3c0,2,1.5,3.5,3.3,3.5c1.2,0,2.2-0.6,2.9-1.8h3.3
      		C11.7,11.1,9.4,12.9,6.4,12.9z"></path>
      </g>
      </symbol><symbol id="c-inactive" viewbox="0 0 12 12.9" enable-background="new 0 0 12 12.9" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M6.3,1.2c-2.8,0-5,2.4-5,5.2c0,3.1,2.3,5.4,5,5.4c1.7,0,3.1-0.7,4.1-2.2h1.5c-1.1,2.1-3.1,3.3-5.5,3.3
      		C2.7,12.9,0,10.1,0,6.4c0-1.7,0.7-3.3,1.9-4.6C3.1,0.6,4.6,0,6.3,0c2.5,0,4.5,1.2,5.7,3.5h-1.5C9.5,2.1,8.2,1.2,6.3,1.2z"></path>
      </g>
      </symbol><symbol id="camera" viewbox="0 0 30.3 21.7" enable-background="new 0 0 30.3 21.7" xml:space="preserve">
      <g>
      	<g>
      		<path fill="currentColor" d="M19.2,2c0.2,0.3,0.5,0.9,0.6,1.3c0.7,1.6,1.7,3.6,3.7,3.6h3.8c0.6,0,1,0.4,1,1v10.9c0,0.6-0.4,1-1,1H3
      			c-0.6,0-1-0.4-1-1V7.8c0-0.6,0.4-1,1-1h3.8c2.1,0,3-2,3.7-3.6c0.2-0.4,0.4-0.9,0.6-1.3H19.2 M19.7,0h-9.1C9,0,8.3,4.8,6.8,4.8H3
      			c-1.7,0-3,1.3-3,3v10.9c0,1.7,1.3,3,3,3h24.3c1.7,0,3-1.3,3-3V7.8c0-1.7-1.3-3-3-3c0,0-3.8,0-3.8,0C22,4.8,21.3,0,19.7,0L19.7,0z"></path>
      	</g>
      	<path fill="currentColor" d="M15.2,8.8c1.9,0,3.4,1.5,3.4,3.4c0,1.9-1.5,3.4-3.4,3.4c-1.9,0-3.4-1.5-3.4-3.4
      		C11.8,10.3,13.3,8.8,15.2,8.8 M15.2,6.8c-3,0-5.4,2.4-5.4,5.4s2.4,5.4,5.4,5.4c3,0,5.4-2.4,5.4-5.4S18.2,6.8,15.2,6.8L15.2,6.8z"></path>
      	<path fill="currentColor" d="M25.5,10.9c-0.3,0-0.5-0.1-0.7-0.3c-0.2-0.2-0.3-0.4-0.3-0.7c0-0.3,0.1-0.5,0.3-0.7c0.4-0.4,1-0.4,1.4,0
      		c0.2,0.2,0.3,0.4,0.3,0.7c0,0.3-0.1,0.5-0.3,0.7C26,10.8,25.8,10.9,25.5,10.9z"></path>
      	<path fill="currentColor" d="M5.5,3.9h-2c-0.6,0-1-0.4-1-1s0.4-1,1-1h2c0.6,0,1,0.4,1,1S6,3.9,5.5,3.9z"></path>
      </g>
      </symbol><symbol id="camera-filled" viewbox="0 0 30.3 21.7" enable-background="new 0 0 30.3 21.7" xml:space="preserve">
      <g>
      	<g>
      		<path fill="currentColor" d="M15.2,8.4c-2.1,0-3.8,1.7-3.8,3.8s1.7,3.8,3.8,3.8c2.1,0,3.8-1.7,3.8-3.8S17.2,8.4,15.2,8.4z"></path>
      		<path fill="currentColor" d="M27.3,4.8c0,0-3.8,0-3.8,0C22,4.8,21.3,0,19.7,0h-9.1C9,0,8.3,4.8,6.8,4.8H3c-1.7,0-3,1.3-3,3v10.9
      			c0,1.7,1.3,3,3,3h24.3c1.7,0,3-1.3,3-3V7.8C30.3,6.2,29,4.8,27.3,4.8z M15.2,17.6c-3,0-5.5-2.4-5.5-5.5s2.4-5.5,5.5-5.5
      			s5.5,2.4,5.5,5.5S18.2,17.6,15.2,17.6z M26.2,10.6c-0.2,0.2-0.5,0.3-0.7,0.3c-0.3,0-0.5-0.1-0.7-0.3c-0.2-0.2-0.3-0.4-0.3-0.7
      			c0-0.3,0.1-0.5,0.3-0.7c0.4-0.4,1-0.4,1.4,0c0.2,0.2,0.3,0.4,0.3,0.7C26.5,10.1,26.4,10.4,26.2,10.6z"></path>
      	</g>
      	<path fill="currentColor" d="M5.5,3.9h-2c-0.6,0-1-0.4-1-1s0.4-1,1-1h2c0.6,0,1,0.4,1,1S6,3.9,5.5,3.9z"></path>
      </g>
      </symbol><symbol id="camera-roll" viewbox="0 0 25 25" enable-background="new 0 0 25 25" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M22,7.7h-4.7V3c0-1.6-1.3-3-3-3H3C1.3,0,0,1.3,0,3v11.3c0,1.6,1.3,3,3,3h4.7V22c0,1.7,1.3,3,3,3H22
      		c1.7,0,3-1.3,3-3V10.7C25,9,23.7,7.7,22,7.7z M7.7,10.7v4.7H3c-0.5,0-1-0.4-1-1V3c0-0.5,0.5-1,1-1h11.3c0.6,0,1,0.5,1,1v4.7h-4.7
      		C9,7.7,7.7,9,7.7,10.7z M23,22c0,0.6-0.5,1-1,1H10.7c-0.5,0-1-0.4-1-1V10.7c0-0.6,0.5-1,1-1H22c0.5,0,1,0.4,1,1V22z"></path>
      	<g>
      		<path fill="currentColor" d="M16.3,20.2c-0.6,0-1-0.4-1-1v-6.3c0-0.6,0.4-1,1-1s1,0.4,1,1v6.3C17.3,19.8,16.9,20.2,16.3,20.2z"></path>
      		<path fill="currentColor" d="M19.5,17h-6.3c-0.6,0-1-0.4-1-1s0.4-1,1-1h6.3c0.6,0,1,0.4,1,1S20.1,17,19.5,17z"></path>
      	</g>
      </g>
      </symbol><symbol id="code" viewbox="0 0 22 11.3" enable-background="new 0 0 22 11.3" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M16.5,11.1c-0.3,0-0.5-0.1-0.7-0.3c-0.4-0.4-0.4-1,0-1.4l3.8-3.8l-3.8-3.8c-0.4-0.4-0.4-1,0-1.4
      		s1-0.4,1.4,0l4.5,4.5c0.4,0.4,0.4,1,0,1.4l-4.5,4.5C17,11,16.7,11.1,16.5,11.1z"></path>
      	<path fill="currentColor" d="M5.5,11.1c-0.3,0-0.5-0.1-0.7-0.3L0.3,6.3C0.1,6.1,0,5.8,0,5.5S0.1,5,0.3,4.8l4.5-4.5c0.4-0.4,1-0.4,1.4,0
      		s0.4,1,0,1.4L2.4,5.5l3.8,3.8c0.4,0.4,0.4,1,0,1.4C6.1,11,5.8,11.1,5.5,11.1z"></path>
      	<path fill="currentColor" d="M9.3,11.3c-0.1,0-0.2,0-0.4-0.1C8.4,11,8.1,10.5,8.3,9.9l3.5-9.1c0.2-0.5,0.8-0.8,1.3-0.6
      		c0.5,0.2,0.8,0.8,0.6,1.3l-3.5,9.1C10.1,11.1,9.7,11.3,9.3,11.3z"></path>
      </g>
      </symbol><symbol id="comment" viewbox="0 0 23 23.8" enable-background="new 0 0 23 23.8" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<g>
      		<path fill="currentColor" d="M19,2c1.1,0,2,0.8,2,1.9v9c0,1-0.9,2.1-2,2.1h-3.4L11,19.2V15H4c-1.1,0-2-1.1-2-2.1v-9C2,2.8,2.9,2,4,2H19
      			 M19,0H4C1.8,0,0,1.7,0,3.9v9C0,15.1,1.8,17,4,17h5v2.2v4.6l3.4-3.1l4-3.7H19c2.2,0,4-1.9,4-4.1v-9C23,1.7,21.2,0,19,0L19,0z"></path>
      	</g>
      </g>
      </symbol><symbol id="comment-add" viewbox="0 0 23 23.8" enable-background="new 0 0 23 23.8" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M12,12.7c-0.6,0-1-0.4-1-1V6.3c0-0.6,0.4-1,1-1s1,0.4,1,1v5.4C13,12.2,12.6,12.7,12,12.7z"></path>
      	<path fill="currentColor" d="M15,10H9c-0.6,0-1-0.4-1-1s0.4-1,1-1h6c0.6,0,1,0.4,1,1S15.6,10,15,10z"></path>
      </g>
      <g>
      	<g>
      		<path fill="currentColor" d="M19,2c1.1,0,2,0.8,2,1.9v9c0,1-0.9,2.1-2,2.1h-3.4L11,19.2V15H4c-1.1,0-2-1.1-2-2.1v-9C2,2.8,2.9,2,4,2H19
      			 M19,0H4C1.8,0,0,1.7,0,3.9v9C0,15.1,1.8,17,4,17h5v2.2v4.6l3.4-3.1l4-3.7H19c2.2,0,4-1.9,4-4.1v-9C23,1.7,21.2,0,19,0L19,0z"></path>
      	</g>
      </g>
      </symbol><symbol id="degree" viewbox="0 0 8.1 8.2" enable-background="new 0 0 8.1 8.2" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M8.1,4.1c0,2.2-1.8,4.1-4.1,4.1c-2.3,0-4-1.8-4-4.1S1.8,0,4.1,0C6.3,0,8.1,1.8,8.1,4.1z M1.7,4.1
      		c0,1.3,1,2.4,2.4,2.4s2.3-1.1,2.3-2.4c0-1.3-1-2.3-2.3-2.3C2.8,1.7,1.7,2.8,1.7,4.1z"></path>
      </g>
      </symbol><symbol id="edit" viewbox="0 0 19.9 21.3" enable-background="new 0 0 19.9 21.3" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M12,21.3H2c-1.1,0-2-0.9-2-2v-14c0-1.1,0.9-2,2-2h5c0.6,0,1,0.4,1,1s-0.4,1-1,1H2v14h10v-5c0-0.6,0.4-1,1-1
      		s1,0.4,1,1v5C14,20.4,13.1,21.3,12,21.3z"></path>
      	<path fill="currentColor" d="M19.3,2.1l-1.6-1.6c-0.8-0.8-2.1-0.8-2.8,0l-1.5,1.5c0,0,0,0,0,0c0,0,0,0,0,0l-7,7C6,9.4,5.8,9.7,5.8,10.1
      		l-0.5,2.1c-0.1,0.6,0,1.2,0.4,1.7c0.4,0.5,0.9,0.8,1.6,0.8c0.2,0,0.3,0,0.5-0.1l2.1-0.5c0.3-0.1,0.7-0.3,0.9-0.5l7-6.9
      		c0,0,0.1,0,0.1-0.1c0,0,0,0,0.1-0.1L19.3,5c0.4-0.4,0.6-0.9,0.6-1.4S19.7,2.5,19.3,2.1z M9.3,12.2l-2.1,0.5l0.5-2.1L14,4.3l1.6,1.6
      		L9.3,12.2z M17,4.4l-1.6-1.6L16.3,2l1.6,1.6L17,4.4z"></path>
      </g>
      </symbol><symbol id="edit-a-copy" viewbox="0 0 21.1 18" enable-background="new 0 0 21.1 18" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M14,18H7c-1.1,0-2-0.9-2-2V7c0-1.1,0.9-2,2-2h2c0.6,0,1,0.4,1,1S9.6,7,9,7H7v9h7v-2c0-0.6,0.4-1,1-1
      		s1,0.4,1,1v2C16,17.1,15.1,18,14,18z"></path>
      	<path fill="currentColor" d="M3,13H2c-1.1,0-2-0.9-2-2V2c0-1.1,0.9-2,2-2h7c1.1,0,2,0.9,2,2v1c0,0.6-0.4,1-1,1S9,3.6,9,3V2H2v9h1
      		c0.6,0,1,0.4,1,1S3.6,13,3,13z"></path>
      	<path fill="currentColor" d="M20.3,4.1L18.2,2c-0.4-0.4-1-0.4-1.4,0l-2.3,2.3L9.9,8.8C9.8,8.9,9.7,9.1,9.6,9.3l-0.7,2.9
      		c-0.2,0.7,0.5,1.4,1.2,1.2l2.9-0.7c0.2-0.1,0.3-0.1,0.5-0.3L18,7.9c0,0,0,0,0,0c0,0,0,0,0,0l2.2-2.2C20.7,5.2,20.7,4.5,20.3,4.1z
      		 M17.5,4.1l0.8,0.8l-0.9,0.9l-0.8-0.8L17.5,4.1z M12.3,10.8l-1,0.2l0.2-1l3.7-3.7l0.8,0.8L12.3,10.8z"></path>
      </g>
      </symbol><symbol id="f-active" viewbox="0 0 6.8 12.4" enable-background="new 0 0 6.8 12.4" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M0,0h6.8v2.7H3v2h3.7v2.8H3v4.9H0V0z"></path>
      </g>
      </symbol><symbol id="f-inactive" viewbox="0 0 6.1 12.4" enable-background="new 0 0 6.1 12.4" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M0,0h6.1v1.2H1.2v4.4h4.9v1.2H1.2v5.6H0V0z"></path>
      </g>
      </symbol><symbol id="facebook" viewbox="0 0 11.4 22" enable-background="new 0 0 11.4 22" xml:space="preserve">
      <path id="aqf_2_" fill="currentColor" d="M7.4,22V12h3.4l0.5-3.9H7.4V5.6c0-1.1,0.3-1.9,1.9-1.9l2.1,0V0.2c-0.4,0-1.6-0.2-3-0.2
      	c-3,0-5,1.8-5,5.2v2.9H0V12h3.4v10H7.4z"></path>
      </symbol><symbol id="google-plus" viewbox="0 0 20 20" enable-background="new 0 0 20 20" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M7.9,4.4C7.7,4.2,7.4,4.1,7,4.1c-0.5,0-0.8,0.2-1.1,0.6C5.6,5,5.5,5.5,5.5,5.9c0,0.6,0.2,1.3,0.5,1.9
      		c0.2,0.3,0.4,0.6,0.7,0.8C7,8.8,7.3,8.9,7.7,8.9c0.4,0,0.8-0.2,1.1-0.5C8.9,8.2,9,8,9,7.8c0-0.2,0-0.4,0-0.6c0-0.7-0.2-1.3-0.5-2
      		C8.4,4.9,8.2,4.6,7.9,4.4z"></path>
      	<path fill="currentColor" d="M0,0v20h20V0H0z M9.7,4.4c0.1,0.1,0.3,0.3,0.4,0.5c0.1,0.2,0.2,0.4,0.3,0.6c0.1,0.2,0.1,0.5,0.1,0.9
      		c0,0.6-0.1,1.1-0.4,1.5C10,8.1,9.9,8.2,9.7,8.4C9.6,8.5,9.4,8.7,9.2,8.8C9.1,8.9,9,9.1,8.9,9.2C8.8,9.3,8.8,9.5,8.8,9.7
      		c0,0.2,0.1,0.3,0.2,0.4c0.1,0.1,0.2,0.2,0.3,0.3l0.6,0.5c0.4,0.3,0.7,0.6,0.9,1c0.3,0.4,0.4,0.8,0.4,1.4c0,0.8-0.4,1.6-1.1,2.2
      		c-0.8,0.7-1.8,1-3.2,1c-1.2,0-2.1-0.3-2.7-0.8c-0.6-0.5-0.9-1-0.9-1.7c0-0.3,0.1-0.7,0.3-1c0.2-0.4,0.5-0.7,1-1
      		c0.5-0.3,1.1-0.5,1.7-0.6c0.6-0.1,1.1-0.1,1.5-0.1c-0.1-0.2-0.2-0.3-0.3-0.5c-0.1-0.2-0.2-0.4-0.2-0.6c0-0.1,0-0.3,0.1-0.4
      		c0-0.1,0.1-0.2,0.1-0.3c-0.2,0-0.4,0-0.5,0C6,9.4,5.3,9.1,4.8,8.6C4.3,8,4.1,7.4,4.1,6.7c0-0.8,0.3-1.6,1-2.3
      		c0.5-0.4,1-0.7,1.5-0.8c0.5-0.1,1-0.2,1.4-0.2h3.4l-1,0.6h-1C9.5,4.2,9.6,4.3,9.7,4.4z M16.8,9.9h-1.8v1.8h-1V9.9h-1.8v0h0v-1h1.8
      		V7h1v1.8h1.8v0h0V9.9z"></path>
      	<path fill="currentColor" d="M7.8,11.7c-0.1,0-0.3,0-0.7,0c-0.4,0-0.7,0.1-1.1,0.2C6,12,5.9,12,5.7,12.1c-0.2,0.1-0.3,0.2-0.5,0.3
      		c-0.2,0.1-0.3,0.3-0.4,0.5c-0.1,0.2-0.2,0.5-0.2,0.8c0,0.6,0.3,1.1,0.8,1.5c0.5,0.4,1.2,0.6,2.1,0.6c0.8,0,1.4-0.2,1.8-0.5
      		c0.4-0.3,0.6-0.8,0.6-1.3c0-0.4-0.1-0.8-0.4-1.1c-0.3-0.3-0.8-0.7-1.4-1.1C8.1,11.7,8,11.7,7.8,11.7z"></path>
      	<rect x="14.9" y="8.8" fill="currentColor" width="1.8" height="0"></rect>
      	<polygon fill="currentColor" points="13.9,8.8 13.9,8.8 12.1,8.8 12.1,9.8 12.1,9.8 12.1,8.8 	"></polygon>
      	<polygon fill="currentColor" points="13.9,7 14.9,7 14.9,8.8 14.9,8.8 14.9,7 13.9,7 13.9,8.8 13.9,8.8 	"></polygon>
      	<polygon fill="currentColor" points="14.9,11.6 13.9,11.6 13.9,9.9 13.9,9.9 13.9,11.6 14.9,11.6 14.9,9.9 14.9,9.9 	"></polygon>
      	<rect x="14.9" y="8.8" fill="currentColor" width="0" height="0"></rect>
      	<rect x="13.9" y="8.8" fill="currentColor" width="0" height="0"></rect>
      	<polyline fill="currentColor" points="13.9,9.9 13.9,9.8 12.1,9.8 12.1,9.9 	"></polyline>
      	<rect x="13.9" y="9.8" fill="currentColor" width="0" height="0"></rect>
      </g>
      </symbol><symbol id="home" viewbox="0 0 20.6 18.5" enable-background="new 0 0 20.6 18.5" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="2425.4" height="1507.5" x="57" y="764.2" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M20.2,7.5l-9.3-7.3c-0.4-0.3-0.9-0.3-1.2,0L0.4,7.5C0,7.9-0.1,8.5,0.2,8.9c0.3,0.4,1,0.5,1.4,0.2l1.2-0.9
      		v9.3c0,0.5,0.4,1,1,1h4.6c0.6,0,1-0.5,1-1v-4.5h1.8v4.5c0,0.5,0.5,1,1,1h4.6c0.6,0,1-0.5,1-1V8.2L19,9.1c0.2,0.1,0.4,0.2,0.6,0.2
      		c0.3,0,0.6-0.1,0.8-0.4C20.8,8.5,20.7,7.9,20.2,7.5z M15.8,16.5h-2.6v-4.5c0-0.6-0.4-1-1-1H8.4c-0.6,0-1,0.4-1,1v4.5H4.8V6.6
      		l5.5-4.3l5.5,4.3V16.5z"></path>
      </g>
      </symbol><symbol id="instagram" viewbox="-782.5 689.9 22.1 22.1" style="enable-background:new -782.5 689.9 22.1 22.1;" xml:space="preserve">
      <g id="atXMLID_7_">
      	<path fill="currentColor" id="atXMLID_8_" d="M-764.7,689.9h-13.6c-2.4,0-4.3,1.9-4.3,4.3v4.5v9c0,2.4,1.9,4.3,4.3,4.3h13.6
      		c2.4,0,4.3-1.9,4.3-4.3v-9v-4.5C-760.4,691.8-762.3,689.9-764.7,689.9z M-763.4,692.4l0.5,0v0.5v3.3l-3.7,0l0-3.7L-763.4,692.4z
      		 M-774.6,698.7c0.7-1,1.9-1.6,3.2-1.6c1.3,0,2.4,0.6,3.2,1.6c0.5,0.6,0.7,1.4,0.7,2.3c0,2.1-1.7,3.9-3.9,3.9
      		c-2.1,0-3.9-1.7-3.9-3.9C-775.3,700.1-775.1,699.3-774.6,698.7z M-762.6,707.7c0,1.2-0.9,2.1-2.1,2.1h-13.6c-1.2,0-2.1-0.9-2.1-2.1
      		v-9h3.3c-0.3,0.7-0.4,1.5-0.4,2.3c0,3.3,2.7,6,6,6s6-2.7,6-6c0-0.8-0.2-1.6-0.4-2.3h3.3V707.7z"></path>
      </g>
      </symbol><symbol id="mail" viewbox="0 0 26 20" enable-background="new 0 0 26 20" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M23,0H3C1.3,0,0,1.3,0,3v14c0,1.7,1.3,3,3,3h20c1.7,0,3-1.3,3-3V3C26,1.3,24.7,0,23,0z M22.4,2L13,9.3
      		L3.6,2H22.4z M24,17c0,0.6-0.5,1-1,1H3c-0.5,0-1-0.4-1-1V3.3l10.4,8.1c0.2,0.1,0.4,0.2,0.6,0.2s0.4-0.1,0.6-0.2L24,3.3V17z"></path>
      </g>
      </symbol><symbol id="menu" viewbox="0 0 18.2 16" enable-background="new 0 0 18.2 16" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M17.2,2H1C0.4,2,0,1.6,0,1s0.4-1,1-1h16.2c0.6,0,1,0.4,1,1S17.7,2,17.2,2z"></path>
      	<path fill="currentColor" d="M17.2,9H1C0.4,9,0,8.6,0,8s0.4-1,1-1h16.2c0.6,0,1,0.4,1,1S17.7,9,17.2,9z"></path>
      	<path fill="currentColor" d="M17.2,16H1c-0.6,0-1-0.4-1-1s0.4-1,1-1h16.2c0.6,0,1,0.4,1,1S17.7,16,17.2,16z"></path>
      </g>
      </symbol><symbol id="more" viewbox="0 0 19.1 4" enable-background="new 0 0 19.1 4" xml:space="preserve">
      <g>
      	<circle fill="currentColor" cx="2" cy="2" r="2"></circle>
      	<circle fill="currentColor" cx="9.6" cy="2" r="2"></circle>
      	<circle fill="currentColor" cx="17.1" cy="2" r="2"></circle>
      </g>
      </symbol><symbol id="movie" viewbox="0 0 24 24" enable-background="new 0 0 24 24" xml:space="preserve">
      <g>
      	<g>
      		<path fill="currentColor" d="M17.6,12c0,0.7-0.4,1.4-1,1.7l-5.3,3c-0.3,0.2-0.6,0.3-1,0.3c-0.4,0-0.7-0.1-1-0.3c-0.6-0.4-1-1-1-1.7V9
      			c0-0.7,0.4-1.4,1-1.7C9.7,7.1,10,7,10.4,7c0.3,0,0.7,0.1,1,0.3l5.3,3C17.3,10.6,17.6,11.3,17.6,12z"></path>
      	</g>
      	<path fill="currentColor" d="M12,2c5.5,0,10,4.5,10,10c0,5.5-4.5,10-10,10C6.5,22,2,17.5,2,12C2,6.5,6.5,2,12,2 M12,0C5.4,0,0,5.4,0,12
      		s5.4,12,12,12s12-5.4,12-12S18.6,0,12,0L12,0z"></path>
      </g>
      </symbol><symbol id="pinterest" viewbox="0 0 20 20" enable-background="new 0 0 20 20" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M10,0C4.5,0,0,4.5,0,10c0,4.1,2.5,7.6,6,9.2c0-0.7,0-1.5,0.2-2.3c0.2-0.8,1.3-5.4,1.3-5.4s-0.3-0.6-0.3-1.6
      		c0-1.5,0.9-2.6,1.9-2.6c0.9,0,1.3,0.7,1.3,1.5c0,0.9-0.6,2.3-0.9,3.5c-0.3,1.1,0.5,1.9,1.6,1.9c1.9,0,3.2-2.4,3.2-5.3
      		c0-2.2-1.5-3.8-4.2-3.8c-3,0-4.9,2.3-4.9,4.8c0,0.9,0.3,1.5,0.7,2C6,12,6.1,12.1,6,12.4c0,0.2-0.2,0.6-0.2,0.8
      		c-0.1,0.3-0.3,0.3-0.5,0.3c-1.4-0.6-2-2.1-2-3.8c0-2.8,2.4-6.2,7.1-6.2c3.8,0,6.3,2.8,6.3,5.7c0,3.9-2.2,6.9-5.4,6.9
      		c-1.1,0-2.1-0.6-2.4-1.2c0,0-0.6,2.3-0.7,2.7c-0.2,0.8-0.6,1.5-1,2.1C8.1,19.9,9,20,10,20c5.5,0,10-4.5,10-10C20,4.5,15.5,0,10,0z"></path>
      </g>
      </symbol><symbol id="play" viewbox="-780.6 689.9 19.8 22.1" style="enable-background:new -780.6 689.9 19.8 22.1;" xml:space="preserve">
      <path fill="currentColor" id="azXMLID_1287_" d="M-780.6,691.2v19.5c0,1,1.1,1.6,1.9,1.1l17.2-9.7c0.9-0.5,0.9-1.8,0-2.3l-17.2-9.7
      	C-779.5,689.6-780.6,690.2-780.6,691.2z"></path>
      </symbol><symbol id="power" viewbox="0 0 18.4 20.5" enable-background="new 0 0 18.4 20.5" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M9.2,10.5C8.5,10.5,8,9.9,8,9.2v-8C8,0.6,8.5,0,9.2,0s1.2,0.6,1.2,1.2v8C10.5,9.9,9.9,10.5,9.2,10.5z"></path>
      	<g>
      		<path fill="currentColor" d="M9.2,20.5c-5.1,0-9.2-4.1-9.2-9.2c0-2.5,1-4.8,2.7-6.5c0.5-0.5,1.3-0.5,1.8,0C5,5.3,5,6.1,4.5,6.6
      			c-1.3,1.3-2,3-2,4.7c0,3.7,3,6.7,6.7,6.7c3.7,0,6.7-3,6.7-6.7c0-1.8-0.7-3.5-2-4.7c-0.5-0.5-0.5-1.3,0-1.8c0.5-0.5,1.3-0.5,1.8,0
      			c1.8,1.7,2.7,4.1,2.7,6.5C18.4,16.4,14.3,20.5,9.2,20.5z"></path>
      	</g>
      </g>
      </symbol><symbol id="print" viewbox="0 0 22 28" enable-background="new 0 0 22 28" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M18.9,2C19.5,2,20,2.5,20,3.1v21.8c0,0.6-0.5,1.1-1.1,1.1H3.1C2.5,26,2,25.5,2,24.9V3.1C2,2.5,2.5,2,3.1,2
      		H18.9 M18.9,0H3.1C1.4,0,0,1.4,0,3.1v21.8C0,26.6,1.4,28,3.1,28h15.8c1.7,0,3.1-1.4,3.1-3.1V3.1C22,1.4,20.6,0,18.9,0L18.9,0z"></path>
      	<g>
      		<rect x="4" y="5" fill="currentColor" width="14" height="2"></rect>
      	</g>
      	<g>
      		<rect x="4" y="9" fill="currentColor" width="14" height="2"></rect>
      	</g>
      	<g>
      		<rect x="4" y="13" fill="currentColor" width="14" height="2"></rect>
      	</g>
      	<g>
      		<rect x="4" y="17" fill="currentColor" width="13" height="2"></rect>
      	</g>
      	<g>
      		<rect x="4" y="21" fill="currentColor" width="8" height="2"></rect>
      	</g>
      </g>
      </symbol><symbol id="rate" viewbox="0 0 35.5 34" enable-background="new 0 0 35.5 34" xml:space="preserve">
      <path fill="currentColor" d="M17.8,2l4.7,10.5l11,1l-8.7,8.1L27.5,32l-9.7-6.1L8,32l2.7-10.5L2,13.5l11-1L17.8,2 M17.8,0
      	c-0.8,0-1.5,0.5-1.8,1.2l-4.2,9.4l-9.9,0.9c-0.8,0.1-1.5,0.6-1.7,1.3c-0.3,0.7,0,1.6,0.5,2.1l7.9,7.3l-2.4,9.3
      	c-0.2,0.8,0.1,1.6,0.8,2.1C7.2,33.9,7.6,34,8,34c0.4,0,0.7-0.1,1.1-0.3l8.7-5.4l8.7,5.4c0.3,0.2,0.7,0.3,1.1,0.3
      	c0.4,0,0.8-0.1,1.2-0.4c0.7-0.5,1-1.3,0.8-2.1L27,22.2l7.9-7.3c0.6-0.5,0.8-1.4,0.5-2.1c-0.3-0.7-0.9-1.3-1.7-1.3l-9.9-0.9l-4.2-9.4
      	C19.3,0.5,18.6,0,17.8,0L17.8,0z"></path>
      </symbol><symbol id="rated" viewbox="0 0 35.5 34" enable-background="new 0 0 35.5 34" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M34.9,14.9L27,22.2l2.4,9.3c0.2,0.8-0.1,1.6-0.8,2.1c-0.3,0.2-0.8,0.4-1.2,0.4c-0.4,0-0.7-0.1-1.1-0.3
      		l-8.7-5.4l-8.7,5.4C8.8,33.9,8.4,34,8,34c-0.4,0-0.8-0.1-1.2-0.4c-0.7-0.5-1-1.3-0.8-2.1l2.4-9.3l-7.9-7.3
      		c-0.6-0.5-0.8-1.4-0.5-2.1c0.2-0.8,0.9-1.3,1.7-1.4l9.9-0.9l4.2-9.4C16.3,0.5,17,0,17.8,0c0.8,0,1.5,0.5,1.8,1.2l4.2,9.4l9.9,0.9
      		c0.8,0.1,1.5,0.6,1.7,1.4C35.7,13.6,35.5,14.4,34.9,14.9z"></path>
      </g>
      </symbol><symbol id="save" viewbox="0 0 15 26" enable-background="new 0 0 15 26" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M11.2,2C12.3,2,13,2.6,13,3v18.4l-4.1-3.9C8.5,17.2,8,17,7.5,17s-1,0.2-1.4,0.5L2,21.4V3c0-0.4,0.7-1,1.8-1
      		H11.2 M11.2,0H3.8C1.7,0,0,1.3,0,3v23l7.5-7l7.5,7V3C15,1.3,13.3,0,11.2,0L11.2,0z"></path>
      </g>
      </symbol><symbol id="saved" viewbox="0 0 15 26" enable-background="new 0 0 15 26" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<g>
      		<path fill="currentColor" d="M15,3v23l-7.5-7L0,26V3c0-1.7,1.7-3,3.8-3h7.5C13.3,0,15,1.3,15,3z"></path>
      	</g>
      </g>
      </symbol><symbol id="share" viewbox="0 0 19.2 19.2" enable-background="new 0 0 19.2 19.2" xml:space="preserve">
      <metadata>
      	<sfw xmlns="&amp;ns_sfw;">
      		<slices></slices>
      		<slicesourcebounds width="3121.4" height="1504.8" x="-639" y="767" bottomleftorigin="true"></slicesourcebounds>
      	</sfw>
      </metadata>
      <g>
      	<path fill="currentColor" d="M16,19.2H2c-1.1,0-2-0.9-2-2v-14c0-1.1,0.9-2,2-2h3c0.6,0,1,0.4,1,1s-0.4,1-1,1H2v14h14v-3c0-0.6,0.4-1,1-1
      		s1,0.4,1,1v3C18,18.3,17.1,19.2,16,19.2z"></path>
      	<path fill="currentColor" d="M19.2,0v7.2c0,0.6-0.5,1-1,1c-0.6,0-1-0.4-1-1V3.4l-8.5,8.5c-0.2,0.2-0.5,0.3-0.7,0.3s-0.5-0.1-0.7-0.3
      		c-0.4-0.4-0.4-1,0-1.4L15.8,2H12c-0.5,0-1-0.4-1-1c0-0.6,0.5-1,1-1H19.2z"></path>
      </g>
      </symbol><symbol id="sides" viewbox="0 0 19.5 19.5" enable-background="new 0 0 19.5 19.5" xml:space="preserve">
      <g>
      	<g>
      		<path fill="currentColor" d="M16.5,19.5c-0.2,0-0.4-0.1-0.5-0.2l-8.7-9.1c-0.7,0.4-1.6,0.2-2.2-0.4L3,7.6C2.8,7.5,2.7,7.3,2.7,7.1
      			c0-0.2,0.1-0.4,0.2-0.5L6.6,3c0.3-0.3,0.8-0.3,1.1,0l2.2,2.2c0.6,0.6,0.7,1.4,0.4,2.2l9.1,8.7c0.2,0.1,0.2,0.3,0.2,0.6
      			c0,0.1,0,1.3-0.8,2.1C17.8,19.5,16.7,19.5,16.5,19.5C16.5,19.5,16.5,19.5,16.5,19.5z M7.4,8.5C7.4,8.5,7.4,8.5,7.4,8.5
      			c0.2,0,0.4,0.1,0.5,0.2l8.9,9.3c0.2-0.1,0.6-0.2,0.8-0.4s0.3-0.5,0.4-0.8L8.7,7.9C8.6,7.8,8.5,7.6,8.5,7.4S8.5,7,8.7,6.9
      			c0.4-0.4,0.1-0.6,0.1-0.7L7.1,4.5L4.5,7.1l1.6,1.6c0.1,0.1,0.2,0.1,0.3,0.1c0.2,0,0.3-0.1,0.4-0.2C7,8.5,7.2,8.5,7.4,8.5z"></path>
      
      			<path fill="currentColor" stroke="#FFFFFF" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" d="
      			M5.3,5.3"></path>
      		<path fill="currentColor" d="M7.1,4.2C6.9,4.2,6.7,4.2,6.6,4L3.9,1.3C3.6,1,3.6,0.5,3.9,0.2s0.8-0.3,1.1,0L7.7,3c0.3,0.3,0.3,0.8,0,1.1
      			C7.5,4.2,7.3,4.2,7.1,4.2z"></path>
      		<path fill="currentColor" d="M5.3,6C5.1,6,4.9,6,4.8,5.8L2,3.1C1.7,2.8,1.7,2.3,2,2s0.8-0.3,1.1,0l2.7,2.7c0.3,0.3,0.3,0.8,0,1.1
      			C5.7,6,5.5,6,5.3,6z"></path>
      		<path fill="currentColor" d="M3.5,7.8C3.3,7.8,3.1,7.8,3,7.6L0.2,4.9c-0.3-0.3-0.3-0.8,0-1.1s0.8-0.3,1.1,0L4,6.6
      			c0.3,0.3,0.3,0.8,0,1.1C3.9,7.8,3.7,7.8,3.5,7.8z"></path>
      	</g>
      	<g>
      		<path fill="currentColor" d="M4,19.5c-0.2,0-1.3,0-2-0.8c-0.8-0.8-0.8-1.9-0.8-2.1c0-0.2,0.1-0.4,0.2-0.5L6,11.7c0.3-0.3,0.8-0.3,1.1,0
      			c0.3,0.3,0.3,0.8,0,1.1L2.7,17c0,0.2,0.1,0.5,0.3,0.7c0.2,0.2,0.5,0.3,0.7,0.3l4.2-4.4c0.3-0.3,0.8-0.3,1.1,0
      			c0.3,0.3,0.3,0.8,0,1.1l-4.5,4.6C4.4,19.4,4.2,19.5,4,19.5C4,19.5,4,19.5,4,19.5z"></path>
      		<g>
      			<path fill="currentColor" d="M14.7,9C14.7,9,14.7,9,14.7,9c-0.9,0-1.6-0.3-2.2-0.9c-0.7-0.7-1-1.6-0.8-2.7c0.1-1,0.6-2,1.4-2.7
      				c0.9-0.9,2.1-1.4,3.2-1.4c0.9,0,1.6,0.3,2.2,0.9c1.3,1.3,1.1,3.8-0.6,5.4C17,8.4,15.9,9,14.7,9z M16.4,2.8c-0.7,0-1.6,0.4-2.2,1
      				c-0.5,0.5-0.9,1.2-1,1.9c-0.1,0.6,0.1,1.1,0.4,1.4c0.3,0.3,0.8,0.4,1.1,0.4c0.7,0,1.6-0.4,2.2-1c1.1-1.1,1.3-2.6,0.6-3.3
      				C17.2,2.8,16.7,2.8,16.4,2.8z"></path>
      		</g>
      	</g>
      </g>
      </symbol><symbol id="spanner" viewbox="0 0 14.6 8.3" enable-background="new 0 0 14.6 8.3" xml:space="preserve">
      <path fill="currentColor" d="M7.3,8.3C7.1,8.3,6.8,8.2,6.6,8L0.3,1.7c-0.4-0.4-0.4-1,0-1.4s1-0.4,1.4,0l5.6,5.6l5.6-5.6
      	c0.4-0.4,1-0.4,1.4,0s0.4,1,0,1.4L8,8C7.8,8.2,7.6,8.3,7.3,8.3z"></path>
      </symbol><symbol id="steps" viewbox="0 0 16.7 16" enable-background="new 0 0 16.7 16" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M15.7,2H4.7c-0.6,0-1-0.4-1-1s0.4-1,1-1h11.1c0.6,0,1,0.4,1,1S16.3,2,15.7,2z"></path>
      	<path fill="currentColor" d="M1,2C0.7,2,0.5,1.9,0.3,1.7C0.1,1.5,0,1.3,0,1c0-0.3,0.1-0.5,0.3-0.7c0.4-0.4,1-0.4,1.4,0
      		C1.9,0.5,2,0.7,2,1c0,0.3-0.1,0.5-0.3,0.7S1.3,2,1,2z"></path>
      	<path fill="currentColor" d="M1,6.7c-0.3,0-0.5-0.1-0.7-0.3C0.1,6.2,0,5.9,0,5.7C0,5.4,0.1,5.1,0.3,5c0.4-0.4,1-0.4,1.4,0
      		C1.9,5.1,2,5.4,2,5.7c0,0.3-0.1,0.5-0.3,0.7C1.5,6.6,1.3,6.7,1,6.7z"></path>
      	<path fill="currentColor" d="M1,11.3c-0.3,0-0.5-0.1-0.7-0.3C0.1,10.9,0,10.6,0,10.3c0-0.3,0.1-0.5,0.3-0.7c0.4-0.4,1-0.4,1.4,0
      		C1.9,9.8,2,10.1,2,10.3c0,0.3-0.1,0.5-0.3,0.7C1.5,11.2,1.3,11.3,1,11.3z"></path>
      	<path fill="currentColor" d="M1,16c-0.3,0-0.5-0.1-0.7-0.3C0.1,15.5,0,15.3,0,15c0-0.3,0.1-0.5,0.3-0.7c0.4-0.4,1-0.4,1.4,0
      		C1.9,14.5,2,14.7,2,15c0,0.3-0.1,0.5-0.3,0.7S1.3,16,1,16z"></path>
      	<path fill="currentColor" d="M12.3,6.7H4.7c-0.6,0-1-0.4-1-1s0.4-1,1-1h7.6c0.6,0,1,0.4,1,1S12.8,6.7,12.3,6.7z"></path>
      	<path fill="currentColor" d="M15.7,11.3H4.7c-0.6,0-1-0.4-1-1s0.4-1,1-1h11.1c0.6,0,1,0.4,1,1S16.3,11.3,15.7,11.3z"></path>
      	<path fill="currentColor" d="M9.3,16H4.7c-0.6,0-1-0.4-1-1s0.4-1,1-1h4.7c0.6,0,1,0.4,1,1S9.9,16,9.3,16z"></path>
      </g>
      </symbol><symbol id="tick" viewbox="0 0 17.2 13.2" enable-background="new 0 0 17.2 13.2" xml:space="preserve">
      <path fill="currentColor" d="M5.5,13.2L0.3,8c-0.4-0.4-0.4-1,0-1.4s1-0.4,1.4,0l3.8,3.8l10-10c0.4-0.4,1-0.4,1.4,0s0.4,1,0,1.4L5.5,13.2z
      	"></path>
      </symbol><symbol id="time-large" viewbox="0 0 28.4 25.2" enable-background="new 0 0 28.4 25.2" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M15.8,25.2c-6.9,0-12.6-5.7-12.6-12.6c0-0.6,0.4-1,1-1s1,0.4,1,1c0,5.8,4.8,10.6,10.6,10.6
      		s10.6-4.8,10.6-10.6S21.6,2,15.8,2c-0.6,0-1-0.4-1-1s0.4-1,1-1c6.9,0,12.6,5.7,12.6,12.6C28.4,19.5,22.7,25.2,15.8,25.2z"></path>
      	<path fill="currentColor" d="M1,14.4c-0.3,0-0.5-0.1-0.7-0.3c-0.4-0.4-0.4-1,0-1.4l3.2-3.2c0.4-0.4,1-0.4,1.4,0s0.4,1,0,1.4l-3.2,3.2
      		C1.5,14.3,1.3,14.4,1,14.4z"></path>
      	<path fill="currentColor" d="M7.3,14.4c-0.3,0-0.5-0.1-0.7-0.3l-3.2-3.2c-0.4-0.4-0.4-1,0-1.4s1-0.4,1.4,0L8,12.7c0.4,0.4,0.4,1,0,1.4
      		C7.8,14.3,7.6,14.4,7.3,14.4z"></path>
      	<path fill="currentColor" d="M12.4,17.4c-0.3,0-0.5-0.1-0.7-0.3c-0.4-0.4-0.4-1,0-1.4l3.1-3.1V6c0-0.6,0.4-1,1-1s1,0.4,1,1V13
      		c0,0.3-0.1,0.5-0.3,0.7l-3.4,3.4C12.9,17.3,12.6,17.4,12.4,17.4z"></path>
      	<circle fill="currentColor" cx="15.8" cy="19.4" r="1"></circle>
      	<circle fill="currentColor" cx="20.7" cy="17.4" r="1"></circle>
      	<circle fill="currentColor" cx="22.8" cy="12.4" r="1"></circle>
      	<circle fill="currentColor" cx="20.7" cy="7.4" r="1"></circle>
      </g>
      </symbol><symbol id="time-small" viewbox="0 0 19.7 17.5" enable-background="new 0 0 19.7 17.5" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M10.9,17.5c-4.8,0-8.8-3.9-8.8-8.8C2.2,8.3,2.5,8,2.9,8s0.8,0.3,0.8,0.8c0,4,3.3,7.2,7.2,7.2
      		s7.2-3.3,7.2-7.2s-3.3-7.2-7.2-7.2c-0.4,0-0.8-0.3-0.8-0.8S10.5,0,10.9,0c4.8,0,8.8,3.9,8.8,8.8S15.8,17.5,10.9,17.5z"></path>
      	<path fill="currentColor" d="M0.8,10C0.6,10,0.4,10,0.2,9.8c-0.3-0.3-0.3-0.8,0-1.1l2.2-2.2c0.3-0.3,0.8-0.3,1.1,0s0.3,0.8,0,1.1
      		L1.3,9.8C1.1,10,0.9,10,0.8,10z"></path>
      	<path fill="currentColor" d="M5.1,10C4.9,10,4.7,10,4.6,9.8L2.4,7.6c-0.3-0.3-0.3-0.8,0-1.1s0.8-0.3,1.1,0l2.2,2.2
      		c0.3,0.3,0.3,0.8,0,1.1C5.5,10,5.3,10,5.1,10z"></path>
      	<path fill="currentColor" d="M8.6,12.1c-0.2,0-0.4-0.1-0.5-0.2c-0.3-0.3-0.3-0.8,0-1.1l2.1-2.1V4.2c0-0.4,0.3-0.8,0.8-0.8
      		s0.8,0.3,0.8,0.8v4.9c0,0.2-0.1,0.4-0.2,0.5l-2.3,2.3C9,12.1,8.8,12.1,8.6,12.1z"></path>
      	<circle fill="currentColor" cx="10.9" cy="13.5" r="0.8"></circle>
      	<circle fill="currentColor" cx="14.4" cy="12" r="0.8"></circle>
      	<circle fill="currentColor" cx="15.8" cy="8.6" r="0.8"></circle>
      	<circle fill="currentColor" cx="14.4" cy="5.2" r="0.8"></circle>
      </g>
      </symbol><symbol id="twitter" viewbox="0 0 27.1 22" enable-background="new 0 0 27.1 22" xml:space="preserve">
      <path fill="currentColor" d="M27.1,2.6c-1,0.4-2.1,0.7-3.2,0.9c1.1-0.7,2-1.8,2.4-3.1c-1.1,0.6-2.3,1.1-3.5,1.3c-1-1.1-2.5-1.8-4.1-1.8
      	c-3.1,0-5.6,2.5-5.6,5.6c0,0.4,0,0.9,0.1,1.3C8.7,6.6,4.6,4.4,1.9,1C1.4,1.8,1.1,2.8,1.1,3.8c0,1.9,1,3.6,2.5,4.6
      	c-0.9,0-1.8-0.3-2.5-0.7c0,0,0,0,0,0.1c0,2.7,1.9,4.9,4.5,5.4c-0.5,0.1-1,0.2-1.5,0.2c-0.4,0-0.7,0-1-0.1c0.7,2.2,2.8,3.8,5.2,3.9
      	c-1.9,1.5-4.3,2.4-6.9,2.4c-0.4,0-0.9,0-1.3-0.1C2.5,21.1,5.4,22,8.5,22c10.2,0,15.8-8.5,15.8-15.8c0-0.2,0-0.5,0-0.7
      	C25.4,4.7,26.3,3.7,27.1,2.6z"></path>
      </symbol><symbol id="wifi-medium" viewbox="0 0 11.3 7.1" enable-background="new 0 0 11.3 7.1" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M10.3,3.9c-0.2,0-0.5-0.1-0.7-0.3C8.5,2.6,7.1,2,5.7,2s-2.9,0.6-4,1.6C1.3,4,0.7,4,0.3,3.6
      		c-0.4-0.4-0.4-1,0-1.4C1.8,0.8,3.7,0,5.7,0c2,0,3.9,0.8,5.3,2.2c0.4,0.4,0.4,1,0,1.4C10.8,3.8,10.6,3.9,10.3,3.9z"></path>
      	<circle fill-rule="evenodd" clip-rule="evenodd" fill="currentColor" cx="5.7" cy="5.6" r="1.5"></circle>
      </g>
      </symbol><symbol id="wifi-protected" viewbox="0 0 11.3 16" enable-background="new 0 0 11.3 16" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M10.3,6.1H9.5V3.9C9.5,1.7,7.8,0,5.6,0C3.5,0,1.8,1.7,1.7,3.9c0,0,0,0,0,0v2.2H1c-0.5,0-1,0.4-1,1V15
      		c0,0.6,0.5,1,1,1h9.3c0.5,0,1-0.4,1-1V7.1C11.3,6.6,10.8,6.1,10.3,6.1z M3.7,3.9C3.7,2.8,4.6,2,5.6,2c0.5,0,1,0.2,1.3,0.6
      		c0.4,0.4,0.6,0.8,0.6,1.3v2.2H3.7V3.9z M9.3,14H2V8.1h7.3V14z"></path>
      </g>
      </symbol><symbol id="wifi-strong" viewbox="0 0 16.9 11.4" enable-background="new 0 0 16.9 11.4" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M15.9,5c-0.3,0-0.5-0.1-0.7-0.3C13.4,3,11,2,8.4,2C5.9,2,3.5,3,1.7,4.7c-0.4,0.4-1,0.4-1.4,0
      		c-0.4-0.4-0.4-1,0-1.4C2.5,1.2,5.4,0,8.4,0c3.1,0,5.9,1.2,8.1,3.3c0.4,0.4,0.4,1,0,1.4C16.4,4.9,16.1,5,15.9,5z"></path>
      	<path fill="currentColor" d="M13.1,8.2c-0.2,0-0.5-0.1-0.7-0.3c-1.1-1-2.5-1.6-4-1.6c-1.5,0-2.9,0.6-4,1.6c-0.4,0.4-1,0.4-1.4,0
      		c-0.4-0.4-0.4-1,0-1.4C4.5,5,6.4,4.3,8.4,4.3c2,0,3.9,0.8,5.3,2.2c0.4,0.4,0.4,1,0,1.4C13.6,8,13.4,8.2,13.1,8.2z"></path>
      	<circle fill-rule="evenodd" clip-rule="evenodd" fill="currentColor" cx="8.4" cy="9.9" r="1.5"></circle>
      </g>
      </symbol><symbol id="wifi-weak" viewbox="0 0 3 3" enable-background="new 0 0 3 3" xml:space="preserve">
      <g>
      	<circle fill-rule="evenodd" clip-rule="evenodd" fill="currentColor" cx="1.5" cy="1.5" r="1.5"></circle>
      </g>
      </symbol><symbol id="x" viewbox="0 0 14 14" enable-background="new 0 0 14 14" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M13.7,12.3c0.4,0.4,0.4,1,0,1.4C13.5,13.9,13.3,14,13,14c-0.3,0-0.5-0.1-0.7-0.3L7,8.4l-5.3,5.3
      		c-0.4,0.4-1,0.4-1.4,0s-0.4-1,0-1.4L5.6,7L0.3,1.7c-0.4-0.4-0.4-1,0-1.4c0.4-0.4,1-0.4,1.4,0L7,5.6l5.3-5.3c0.4-0.4,1-0.4,1.4,0
      		c0.4,0.4,0.4,1,0,1.4L8.4,7L13.7,12.3z"></path>
      </g>
      </symbol><symbol id="collection" viewbox="0 0 22 22" enable-background="new 0 0 22 22" xml:space="preserve">
      <g>
      	<path fill="currentColor" d="M21,8H18V5a1,1,0,0,0-1-1H14V1a1,1,0,0,0-.95-1H1A1,1,0,0,0,0,1V13.05A1,1,0,0,0,1,14H4v3a1,1,0,0,0,1,1H8v3a1,1,0,0,0,.95,1H21a1,1,0,0,0,1-1V8.94A1,1,0,0,0,21,8ZM2,12V2H12V4H5A1,1,0,0,0,4,5v7H2Zm4,4V6H16V8H8.95A0.91,0.91,0,0,0,8,8.94V16H6Zm14,4H10V10H20V20Z"></path>
      </g>
      </symbol></svg>
    </div>
    <modals active-modal='app.activeModal' show-modals='app.showModals'></modals>
    <alerts></alerts>
    <div ui-view='nav'></div>
    <div data-anim-speed='250' id='mainView' ng-class="{'show-flag': app.showFlag}" ng-cloak="" ui-view='main'></div>
    <footer>
      <div class='anim-slide-below-fade' ui-view='footer'></div>
    </footer>
  </body>
</html>

#endif
		#endregion

		HtmlNode GetIngredientsDiv()
		{
			var result = base.GetNode(DIV, "ingredients-wrapper");
			return result;
		}

		List<HtmlNode> GetIngredientGroups(HtmlNode div)
		{
			List<HtmlNode> result = null;

			var lis = div.Descendants(LI);
			result = lis.ByClass("ingredient-group").ToList();

			return result;
		}

		List<string> GetIngredients(HtmlNode ingredientGroup)
		{
			var result = new List<string>();

			if (null != ingredientGroup)
			{
				var strong = ingredientGroup.Descendants("strong").First();
				result.Add(strong.InnerText.FromHtml());

				foreach (var li in ingredientGroup.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
						result.Add(li.InnerText.FromHtml());
					}
				}
			}

			return result;
		}


		override protected void GetProcedures()
		{
			var div = GetProceduresDiv();
			if (null != div)
			{
				var preparationGroups = this.GetPreparationGroups(div);
				if (null != preparationGroups)
				{
					foreach (var preparationGroup in preparationGroups)
					{
						var pg = new ProcedureGroup();
						this.Add(pg);
						var preparation = this.GetProcedures(preparationGroup);
						foreach (var procedure in preparation)
						{
							var pgi = new ProcedureItem(procedure);
							this.Add(pgi);
						}
					}
				}
			}
		}

		HtmlNode GetProceduresDiv()
		{
			var result = base.GetNode(DIV, "instructions");
			return result;
		}

		List<HtmlNode> GetPreparationGroups(HtmlNode div)
		{
			var result = div.Descendants(LI).ByClass("preparation-group").ToList();
			return result;
		}

		List<string> GetProcedures(HtmlNode preparationGroup)
		{
			var result = new List<string>();

			if (null != preparationGroup)
			{
				var strong = preparationGroup.Descendants("strong").First();
				result.Add(strong.InnerText.FromHtml());

				foreach (var li in preparationGroup.Descendants(LI))
				{
					if (li.NodeType == HtmlNodeType.Element)
					{
						result.Add(li.InnerText.FromHtml());
					}
				}
			}

			return result;
		}


	}//class
}//ns
