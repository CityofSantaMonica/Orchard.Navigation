(function($) {
  //this plugin wraps the $.fn.toc plugin with some customizations
  $.fn.tableOfContents = function(options) {
    var self = this;
    var opts = $.extend({}, $.fn.tableOfContents.defaults, options);

    var selectors = [];
    for (i = opts.startLevel; i <= opts.endLevel; i++) {
      selectors.push("h" + i);
    }

    //apply the $.fn.toc plugin
    //do it now, because we may modify 'self' later
    self.toc({
      selectors: selectors.join(),
      container: opts.container,
      highlightOffset: 100,
    });

    var timeout;
    function clearTocActiveOnTimeout(ms) {
      if (timeout) {
        clearTimeout(timeout);
      }
      //the toc plugin watches scolling every 50ms
      //so default to 1ms after this
      ms = ms || 51;
      timeout = window.setTimeout(
          function() {
            $("li", self).removeClass("toc-active");
          },
          ms
      );
    }

    $(window).bind("scroll", function() {
      //if we scroll to the top, remove the active class
      if ($(this).scrollTop() === 0) {
        clearTocActiveOnTimeout();
      }
    });

    //clear the initially applied active class when this plugin first loads
    clearTocActiveOnTimeout();

    if (opts.affix) {
      //make the header follow with the toc
      self.siblings("header")
          .remove()
          .prependTo(self);
      //apply the bootstrap affix plugin
      self.affix({ offset: { top: 0, bottom: 0 } });
      //resize the toc to its wrapper's width
      function resize() {
        self.width(self.closest("div").width());
      }
      //do it on bootstrap events
      self.on("affix.bs.affix affix-top.bs.affix affix-bottom.bs.affix", function() {
        resize();
      });
      //and on window resize
      $(window).on("resize", resize);
      //and right now (when this plugin loads)
      resize();
      //generate a 'Back to Top' link
      if (opts.makeTopLink) {
        $("body").prepend($("<a />").attr("id", "top").addClass("invisible"));
        self.append($("<a />").addClass("toc-top").attr("href", "#top").text(opts.topLinkText));
      }
    }
  };

  $.fn.tableOfContents.defaults = {
    startLevel: 2,
    endLevel: 3,
    affix: false,
    makeTopLink: false,
    topLinkText: "Back to top",
  };
})(jQuery);
