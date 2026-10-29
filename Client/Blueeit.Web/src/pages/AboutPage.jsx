import Block from "../components/Block";
import BlockContent from "../components/BlockContent";
import BlockHeader from "../components/BlockHeader";
import BlockItem from "../components/BlockItem";
import BlockSpacer from "../components/BlockSpacer";
import BodyHeader from "../components/BodyHeader";
import PageLayout from "../components/PageLayout";
import HelpSideNavPanel from "../components/panels/HelpSideNavPanel";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";

export default function AboutPage() {
    const sideNavSelectionIndex = 1;

    return (
        <>
            <BodyHeader title={"About"}/>
            <PageLayout sidebar={<><OnlineMembersPanel/></>} sidenav={<><HelpSideNavPanel selectionIndex={sideNavSelectionIndex}/></>}>
                <Block>
                    <BlockContent>
                        <>
                            <section className="about-section"> <h1>About Blueeit</h1> <p> Blueeit is a community forum built for discussion, conversation, and sharing ideas. </p> <p> Browse forums, take part in discussions, and connect with other members of the community. </p> </section> <section className="about-section"> <h1>Our Community</h1> <p> Blueeit is designed around simple, organized discussions. Create threads, share your thoughts, reply to others, and take part in the conversations that interest you. </p> </section> <section className="about-section"> <h1>Built with</h1> <ul> <li>ASP.NET Core Web API</li> <li>C#</li> <li>React</li> <li>PostgreSQL</li> </ul> </section> <section className="about-section"> <h1>About the Project</h1> <p> Blueeit is a personal project focused on building a complete full-stack web application while exploring modern backend and frontend development. </p> </section>
                        </>
                    </BlockContent>
                </Block>
            </PageLayout>
        </>
    );
}