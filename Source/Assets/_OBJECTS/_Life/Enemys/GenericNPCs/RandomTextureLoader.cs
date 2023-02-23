using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTextureLoader : MonoBehaviour
{
    public List<Texture2D> hairTextures;
    public List<Texture2D> pantsTextures;
    public List<Texture2D> shirtTextures;
    public List<Texture2D> shoesTextures;
    public List<Texture2D> skinTextures;


    public void SetTexture(SkinnedMeshRenderer targetMesh)
    {
        Texture2D hair = GetRandomElement(hairTextures);
        Texture2D pants = GetRandomElement(pantsTextures);
        Texture2D shirt = GetRandomElement(shirtTextures);
        Texture2D shoes = GetRandomElement(shoesTextures);
        Texture2D skin = GetRandomElement(skinTextures);

        Texture2D texture2 = MergeTextureList(new List<Texture2D> { hair, pants, shirt, shoes, skin });

        targetMesh.material.mainTexture = texture2;
    }
    Texture2D GetRandomElement(List<Texture2D> sprites)
    {
        return sprites[Random.Range(0, sprites.Count)];
    }

    void SetTextures()
    {
        Texture2D hair = GetRandomElement(hairTextures);
        Texture2D pants = GetRandomElement(pantsTextures);
        Texture2D shirt = GetRandomElement(shirtTextures);
        Texture2D shoes = GetRandomElement(shoesTextures);
        Texture2D skin = GetRandomElement(skinTextures);

        //Texture2D texture = MergeTextures(hair, pants);
        //texture = MergeTextures(texture, shirt);
        //texture = MergeTextures(texture, shoes);
        //texture = MergeTextures(texture, skin);

        Texture2D texture2 = MergeTextureList(new List<Texture2D> { hair, pants, shirt, shoes, skin });

        //mesh.material.mainTexture = texture2;

    }

    Texture2D MergeTextures(Texture2D t1, Texture2D t2)
    {
        Texture2D mergedTexture = new Texture2D(t1.width, t1.height);

        for (int x = 0; x < mergedTexture.width; x++)
        {
            for (int y = 0; y < mergedTexture.height; y++)
            {
                Color pixelT1 = t1.GetPixel(x,y);
                Color pixelT2 = t2.GetPixel(x,y);

                Color mergedPixel = Color.Lerp(pixelT1, pixelT2, pixelT2.a / 1);

                mergedTexture.SetPixel(x, y, mergedPixel);
            }
        }
        mergedTexture.Apply();
        return mergedTexture;
    }

    Texture2D MergeTextureList(List<Texture2D> textures)
    {
        Texture2D mergedTexture = new Texture2D(textures[0].width, textures[0].height);

        for (int x = 0; x < mergedTexture.width; x++)
        {
            for (int y = 0; y < mergedTexture.height; y++)
            {
                Color mergedPixel;

                List<Color> pixels = new List<Color>();
                foreach (var texture in textures)
                {
                    Color pixel = texture.GetPixel(x, y);
                    pixels.Add(pixel);
                }

                for (int i = 0; i < pixels.Count-1; i++)
                {
                    mergedPixel = Color.Lerp(pixels[i], pixels[i+1], pixels[i+1].a / 1);
                    pixels[i+1] = mergedPixel;
                }
                mergedPixel = pixels[pixels.Count - 1];
                mergedTexture.SetPixel(x, y, mergedPixel);
            }
        }
        mergedTexture.Apply();
        return mergedTexture;
    }
}
