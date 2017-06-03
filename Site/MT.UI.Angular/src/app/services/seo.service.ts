
import { Injectable } from "@angular/core";
import { Title,Meta,By } from "@angular/platform-browser";
import { StringUtils } from "app/utils/string.utils";
import { ɵgetDOM as getDOM } from "@angular/platform-browser";

@Injectable()
export class SeoService {
    private titleService: Title;
    private headElement: HTMLElement;
    private metaDescription: HTMLElement;
    private metaKeywords: HTMLElement;
    private robots: HTMLElement;
    private DOM: any;
    private metatags:Meta;

    public constructor(titleService: Title , metatags:Meta ){
        this.titleService = titleService;
        this.DOM = document;//getDOM();
        this.headElement = this.DOM.querySelector('head'); // this.DOM. ('head','head');
        
        this.setTitle('');
        this.metatags = metatags;
        
    }

    public setSeoData(seoModel: SeoModel){
        this.setTitle(seoModel.title);
        this.setMetaRobots(seoModel.robots)
        this.setMetaDescription(seoModel.description)
        this.setMetaKeywords(seoModel.keywords)
    }

    private setTitle(newTitle: string){
        if(StringUtils.isNullOrEmpty(newTitle)) 
        {
            newTitle = "Defina um Título"
        }
        this.titleService.setTitle(newTitle + " - AppExemplo")
    }

    private setMetaDescription(description: string) {
        this.metaDescription = this.getOrCreateMetaElement('description');
        if (StringUtils.isNullOrEmpty(description)) 
        {
             description = "Aqui você encontra um evento técnico próximo de você" 
        }
        this.metaDescription.setAttribute('content', description);
    }

    private setMetaKeywords(keywords: string) {
        this.metaKeywords = this.getOrCreateMetaElement('keywords');
        if (StringUtils.isNullOrEmpty(keywords)) 
        { 
            keywords = "eventos,workshops,encontros,congressos,comunidades,tecnologia" 
        }
        this.metaKeywords.setAttribute('content', keywords);
    }

    private setMetaRobots(robots: string) {
        this.robots = this.getOrCreateMetaElement('robots');
        if (StringUtils.isNullOrEmpty(robots)) { 
            robots = "all"
         }
        this.robots.setAttribute('content', robots);
    }

    private getOrCreateMetaElement(name: string): HTMLElement {
        let el: HTMLElement;
        el = this.DOM.querySelector('meta[name=' + name + ']');
        if (el === null) {
            el = this.DOM.createElement('meta');
            el.setAttribute('name', name);
            this.headElement.appendChild(el);
        }
        return el;
    }
}

export class SeoModel{
    public title: string = '';
    public description: string = '';
    public robots: string = '';
    public keywords: string = '';
}